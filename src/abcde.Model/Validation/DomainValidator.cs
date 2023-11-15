using FluentValidation;

namespace abcde.Model.Validation
{
    public class DomainValidator : AbstractValidator<Domain>
    {
        public DomainValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("NameRequired")
                .MaximumLength(128).WithMessage("NameMustBeAtLeast128Characters");            
        }
    }
}
