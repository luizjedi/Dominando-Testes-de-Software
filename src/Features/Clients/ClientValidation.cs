using FluentValidation;
using System;

namespace Features.Clients
{
    public class ClientValidation : AbstractValidator<Client>
    {
        public ClientValidation()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Please, make sure you have entered the name!")
                .Length(2, 50).WithMessage("The name must be between 2 and 50 characters");

            RuleFor(c => c.LastName)
                .NotEmpty().WithMessage("Please, make sure you have entered the last name!")
                .Length(2, 50).WithMessage("The last name must be between 2 and 50 characters");

            RuleFor(c => c.BirthDate)
                .NotEmpty()
                .Must(HaveMinimumAge)
                .WithMessage("The client must be 18 years or more");

            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }

        public static bool HaveMinimumAge(DateTime birthDate)
        {
            return birthDate <= DateTime.Now.AddYears(-18);
        }
    }
}
