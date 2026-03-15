using FluentValidation;
using Tank_Wiki.DTOs.Tank;

namespace Tank_Wiki.Validation;

public class CreateTankDtoValidator : AbstractValidator<CreateTankDto>
{
    public CreateTankDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Country)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Type)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.ProductionYear)
            .InclusiveBetween(1900, DateTime.Now.Year);

        RuleFor(x => x.Weight)
            .GreaterThan(0);

        RuleFor(x => x.MainGun)
            .NotEmpty();

        RuleFor(x => x.Crew)
            .InclusiveBetween(1, 10);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(999);

        RuleFor(x => x.ImageUrl)
            .NotEmpty()
            .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
            .WithMessage("ImageUrl düzgün URL olmalıdır");
    }
}