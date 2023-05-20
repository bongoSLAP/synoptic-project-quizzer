using FluentValidation;
using Quizzer.Models.Entities.Info;

namespace Quizzer.Validators;

public class QuestionInfoValidator : AbstractValidator<QuestionInfo>
{
    public QuestionInfoValidator()
    {
        RuleFor(q => q.Text).NotEmpty().WithMessage("Question text is required");
        RuleFor(q => q.QuestionIndex).GreaterThanOrEqualTo(0).WithMessage("Question index cant be negative.");
        RuleForEach(q => q.Answers).SetValidator(new AnswerInfoValidator());
    }
}