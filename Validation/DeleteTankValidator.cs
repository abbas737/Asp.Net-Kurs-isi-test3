using FluentValidation;

namespace Tank_Wiki.Validation;

public class DeleteTankValidator : AbstractValidator<int>
{
    public DeleteTankValidator()
    {
        RuleFor(id => id)
            .GreaterThan(0)
            .WithMessage("Tank Id must be greater than 0.");
    }
}