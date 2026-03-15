using FluentValidation;

namespace Tank_Wiki.Validation;

public class GetTankByIdValidator : AbstractValidator<int>
{
    public GetTankByIdValidator()
    {
        RuleFor(id => id)
            .GreaterThan(0)
            .WithMessage("Tank Id must be greater than 0.");
    }
}