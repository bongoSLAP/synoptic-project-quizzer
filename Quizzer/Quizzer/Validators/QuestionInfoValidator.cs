using FluentValidation;
using Quizzer.Models.Entities.Info;

namespace Quizzer.Validators;

public class QuestionInfoValidator : AbstractValidator<QuestionInfo>
{
    public QuestionInfoValidator()
    {
        RuleFor(q => q.Text).NotEmpty();
        RuleFor(q => q.QuestionIndex).GreaterThan(0);
        RuleForEach(q => q.Answers).SetValidator(new AnswerInfoValidator());
    }
}