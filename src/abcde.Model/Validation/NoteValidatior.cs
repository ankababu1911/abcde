using FluentValidation;

namespace abcde.Model.Validation
{
    public class NoteValidator : AbstractValidator<Note>
    {
        public NoteValidator()
        {
            RuleFor(x => x.NoteText).NotEmpty().WithMessage("Note required");
        }
    }
}
