using FluentValidation;
using abcde.Model.ViewModels;

namespace abcde.Model.Validation
{
    public class WorkItemValidator : AbstractValidator<WorkItemViewModel>
    {
        public WorkItemValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("TitleRequired")
                .MaximumLength(128).WithMessage("TitleMustBeAtLeast128Characters");
            RuleFor(x=> x.Description)
                .MaximumLength(1024).WithMessage("DescriptionMustBeAtLeast1024Characters"); 
        }
    }
}
