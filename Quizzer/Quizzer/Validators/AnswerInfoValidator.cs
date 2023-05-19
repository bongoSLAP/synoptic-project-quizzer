using FluentValidation;
using Quizzer.Models.Entities.Info;

namespace Quizzer.Validators;

public class AnswerInfoValidator : AbstractValidator<AnswerInfo>
{
    public AnswerInfoValidator()
    {
        RuleFor(a => a.Text).NotEmpty().WithMessage("Answer text is required.");
        RuleFor(a => a.AnswerIndex).GreaterThanOrEqualTo(0).WithMessage("Answer index greater than zero.");
    }
}