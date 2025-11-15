using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.DTOs.Driver.Validators;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Driver.Commands.CreateDriver;

public class CreateDriverCommandHandler : IRequestHandler<CreateDriverCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateDriverCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateDriverCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateDriverDtoValidator();
        var validationResult = await validator.ValidateAsync(request.DriverDto, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult);

        var driver = _mapper.Map<Domain.Entities.Driver>(request.DriverDto);

        await _unitOfWork.DriverRepository.CreateAsync(driver);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return driver.Id;
    }
}