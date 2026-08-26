using ComicTracker.Application.DTOs.Comics;
using FluentValidation;

namespace ComicTracker.Application.Validators
{
    public class CreateComicValidator : AbstractValidator<CreateUpdateComicDto>
    {
        public CreateComicValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Writer).NotEmpty().MaximumLength(150);
            RuleFor(x => x.Artist).NotEmpty().MaximumLength(150);            
        }
    }
}
