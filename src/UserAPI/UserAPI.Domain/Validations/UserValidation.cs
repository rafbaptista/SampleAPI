using FluentValidation;
using UserAPI.Domain.Entities;

namespace UserAPI.Domain.Validations
{
    public class UserValidation : AbstractValidator<User>
    {
        public UserValidation()
        {
            ValidateId();
            ValidateEmail();
            ValidateName();
            ValidatePassword();
        }

        private void ValidateId()
        {
            RuleFor(u => u.Id).NotNull();
        }

        private void ValidateEmail()
        {
            RuleFor(u => u.Email).NotNull().MinimumLength(3);
        }

        private void ValidateName()
        {
            RuleFor(u => u.Name).NotNull().MinimumLength(3);
        }

        private void ValidatePassword()
        {
            RuleFor(u => u.Password).NotNull().MinimumLength(3);
        }
    }
}
