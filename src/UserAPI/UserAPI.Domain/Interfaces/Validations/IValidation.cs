using FluentValidation.Results;

namespace UserAPI.Domain.Interfaces.Validations
{
    public interface IValidation
    {
        ValidationResult Validate();
    }
}
