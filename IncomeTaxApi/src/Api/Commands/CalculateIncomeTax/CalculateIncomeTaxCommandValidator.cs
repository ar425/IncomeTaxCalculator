using FluentValidation;

namespace IncomeTaxApi.Api.Commands.CalculateIncomeTax
{

    // separate validation class with fluentValidation to allow for custom validation and clean code
    public class CalculateIncomeTaxCommandValidator : AbstractValidator<CalculateIncomeTaxCommand>
    {
        public CalculateIncomeTaxCommandValidator()
        {
            RuleFor(x => x.AnnualSalaryAmount)
                .GreaterThan(0)
                .NotEmpty()
                .WithMessage("Value cannot be empty or zero");
        }

    }
}