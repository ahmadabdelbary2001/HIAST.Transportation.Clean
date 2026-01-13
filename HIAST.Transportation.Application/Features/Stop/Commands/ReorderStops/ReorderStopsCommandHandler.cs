using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HIAST.Transportation.Application.Features.Stop.Commands.ReorderStops;

public class ReorderStopsCommandHandler : IRequestHandler<ReorderStopsCommand, Unit>
{
    private readonly IStopRepository _stopRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ReorderStopsCommandHandler(IStopRepository stopRepository, IUnitOfWork unitOfWork)
    {
        _stopRepository = stopRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(ReorderStopsCommand request, CancellationToken cancellationToken)
    {
        var dto = request.ReorderDto;

        // التحقق من أن جميع المحطات المرسلة تنتمي إلى الخط المحدد
        var allStopsInLine = (await _stopRepository.GetAllAsync())
            .Where(s => s.LineId == dto.LineId)
            .ToList();

        if (!allStopsInLine.Any())
        {
            throw new ArgumentException($"Line {dto.LineId} has no stops");
        }

        // التحقق من أن جميع IDs المرسلة موجودة في الخط
        var sentStopIds = dto.Stops.Select(s => s.Id).ToHashSet();
        var lineStopIds = allStopsInLine.Select(s => s.Id).ToHashSet();
        
        foreach (var stopId in sentStopIds)
        {
            if (!lineStopIds.Contains(stopId))
            {
                throw new ArgumentException($"Stop {stopId} does not belong to line {dto.LineId}");
            }
        }

        // بدء معاملة قاعدة البيانات
        await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            // أولاً: إعادة تعيين جميع المحطات في الخط إلى قيم تسلسلية مؤقتة عالية
            // لتجنب انتهاك القيد الفريد أثناء التحديث
            int tempOrder = 1000;
            foreach (var stop in allStopsInLine)
            {
                stop.SequenceOrder = tempOrder++;
                await _stopRepository.UpdateAsync(stop);
            }

            // حفظ التغييرات المؤقتة
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // ثانياً: تحديث المحطات المرسلة بالقيم الجديدة
            foreach (var stopOrder in dto.Stops)
            {
                var stop = allStopsInLine.FirstOrDefault(s => s.Id == stopOrder.Id);
                if (stop != null)
                {
                    stop.SequenceOrder = stopOrder.SequenceOrder;
                    stop.StopType = stopOrder.StopType;
                    await _stopRepository.UpdateAsync(stop);
                }
            }

            // ثالثاً: المحطات التي لم ترسل - نعطيها تسلسلاً عاليًا مؤقتًا
            var updatedIds = dto.Stops.Select(s => s.Id).ToHashSet();
            var unsentStops = allStopsInLine.Where(s => !updatedIds.Contains(s.Id)).ToList();
            
            foreach (var stop in unsentStops)
            {
                stop.SequenceOrder = tempOrder++;
                await _stopRepository.UpdateAsync(stop);
            }

            // حفظ التغييرات
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // رابعاً: إعادة ترتيب جميع المحطات في الخط لتصبح متتابعة (1, 2, 3, ...)
            var finalStops = (await _stopRepository.GetAllAsync())
                .Where(s => s.LineId == dto.LineId)
                .OrderBy(s => s.SequenceOrder)
                .ToList();

            for (int i = 0; i < finalStops.Count; i++)
            {
                if (finalStops[i].SequenceOrder != i + 1)
                {
                    finalStops[i].SequenceOrder = i + 1;
                    await _stopRepository.UpdateAsync(finalStops[i]);
                }
            }

            // حفظ التغييرات
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // خامساً: التأكد من أن المحطة الأخيرة فقط هي Terminus
            var orderedStops = (await _stopRepository.GetAllAsync())
                .Where(s => s.LineId == dto.LineId)
                .OrderBy(s => s.SequenceOrder)
                .ToList();

            for (int i = 0; i < orderedStops.Count; i++)
            {
                var shouldBeTerminus = (i == orderedStops.Count - 1);
                var isTerminus = orderedStops[i].StopType == StopType.Terminus;

                if (shouldBeTerminus && !isTerminus)
                {
                    orderedStops[i].StopType = StopType.Terminus;
                    await _stopRepository.UpdateAsync(orderedStops[i]);
                }
                else if (!shouldBeTerminus && isTerminus)
                {
                    orderedStops[i].StopType = StopType.Intermediate;
                    await _stopRepository.UpdateAsync(orderedStops[i]);
                }
            }

            // حفظ التغييرات النهائية
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // تأكيد المعاملة
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            // التراجع عن المعاملة في حالة حدوث خطأ
            await transaction.RollbackAsync(cancellationToken);
            throw new Exception($"Failed to reorder stops: {ex.Message}", ex);
        }

        return Unit.Value;
    }
}