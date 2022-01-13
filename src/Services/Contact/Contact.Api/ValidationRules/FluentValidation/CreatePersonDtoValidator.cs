using Contact.Domain.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.Api.ValidationRules.FluentValidation
{
    public class CreatePersonDtoValidator:AbstractValidator<CreatePersonDto>
    {
        public CreatePersonDtoValidator()
        {
            RuleFor(p => p.Name).NotNull().NotEmpty().MinimumLength(3);
            RuleFor(p => p.Surname).NotNull().NotEmpty().MinimumLength(3);
            RuleFor(p => p.Company).NotNull().NotEmpty();

            RuleFor(p => p.Latitude).NotNull().NotEmpty();
            RuleFor(p => p.Latitude).GreaterThanOrEqualTo(-90).LessThanOrEqualTo(90).WithMessage("Longitude must be between -90 and 90 degrees inclusive.");

            RuleFor(p => p.Longitude).NotNull().NotEmpty();
            RuleFor(p => p.Longitude).GreaterThanOrEqualTo(-180).LessThanOrEqualTo(180).WithMessage("Longitude must be between -180 and 180 degrees inclusive.");            
        }
    }
}
