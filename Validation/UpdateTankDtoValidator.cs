using FluentValidation;
using Tank_Wiki.DTOs.Tank;

namespace Tank_Wiki.Validation;

public class UpdateTankDtoValidator : AbstractValidator<UpdateTankDto>
{
    public UpdateTankDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("Tank name cannot be empty and must be at most 100 characters.");

        RuleFor(x => x.Country)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("Tank country cannot be empty and must be at most 100 characters.");

        RuleFor(x => x.Type)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("Tank type cannot be empty and must be at most 50 characters.");

        RuleFor(x => x.ProductionYear)
            .InclusiveBetween(1900, DateTime.Now.Year)
            .WithMessage("Production year must be valid.");

        RuleFor(x => x.Weight)
            .GreaterThan(0)
            .WithMessage("Tank weight must be greater than 0.");

        RuleFor(x => x.MainGun)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("Main gun cannot be empty and must be at most 100 characters.");

        RuleFor(x => x.Crew)
            .GreaterThan(0)
            .WithMessage("Crew count must be greater than 0.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(1000)
            .WithMessage("Description cannot be empty and must be at most 1000 characters.");

        RuleFor(x => x.ImageUrl)
            .NotEmpty()
            .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
            .WithMessage("Image URL must be a valid absolute URL.");

        RuleFor(x => x.VideoUrl)
            .Must(url => Uri.TryCreate(url!, UriKind.Absolute, out _))
            .When(x => !string.IsNullOrEmpty(x.VideoUrl))
            .WithMessage("Video URL must be a valid absolute URL.");
    }
}