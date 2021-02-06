using FluentValidation.Results;
using System;
using UserAPI.Domain.Enums;
using UserAPI.Domain.Interfaces;
using UserAPI.Domain.Interfaces.Validations;
using UserAPI.Domain.Validations;

namespace UserAPI.Domain.Entities
{
    public class User : IEntity, IValidation
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }

        public string CPF { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Cellphone { get; set; }

        public EGender Gender { get; set; }
        public bool Excluded { get; set; }

        public string Role { get; set; }

        public Job Job { get; set; }
        public Guid JobId { get; set; }

        public ValidationResult Validate()
        {
            return new UserValidation().Validate(this);
        }
    }
}
