// HIAST.Transportation.Application.Exceptions/BadRequestException.cs
using FluentValidation.Results;

namespace HIAST.Transportation.Application.Exceptions;

public class BadRequestException : Exception
{
    public IDictionary<string, string[]> ValidationErrors { get; set; }

    public BadRequestException(string message) : base(message)
    {
        ValidationErrors = new Dictionary<string, string[]>();
    }

    public BadRequestException(string message, ValidationResult validationResult) : base(message)
    {
        ValidationErrors = validationResult.Errors
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }
}