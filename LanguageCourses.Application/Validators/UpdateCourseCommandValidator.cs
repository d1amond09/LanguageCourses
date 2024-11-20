using FluentValidation;
using FluentValidation.Results;
using LanguageCourses.Application.Commands;

namespace LanguageCourses.Application.Validators;

public sealed class UpdateCourseCommandValidator :
    AbstractValidator<UpdateCourseCommand>
{
    public UpdateCourseCommandValidator()
    {
        RuleFor(c => c.Course.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(1, 100).WithMessage("Name must be between 1 and 100 characters.");

        RuleFor(c => c.Course.GroupSize)
            .NotEmpty().WithMessage("GroupSize is required.")
            .GreaterThan(0).WithMessage("GroupSize must be greater than 0.");

        RuleFor(c => c.Course.Hours)
            .NotEmpty().WithMessage("Hours is required.")
            .GreaterThan(0).WithMessage("Hours must be greater than 0.");

        RuleFor(c => c.Course.TuitionFee)
            .NotEmpty().WithMessage("TuitionFee is required.")
            .GreaterThan(0).WithMessage("TuitionFee must be greater than 0.");

        RuleFor(c => c.Course.Intensity)
            .NotEmpty().WithMessage("Intensity is required.")
            .Length(1, 250).WithMessage("Intensity must be between 1 and 250 characters.");

        RuleFor(c => c.Course.TrainingProgram)
            .NotEmpty().WithMessage("TrainingProgram is required.")
            .Length(1, 250).WithMessage("TrainingProgram must be between 1 and 250 characters.");

        RuleFor(c => c.Course.Description)
            .Length(1, 500).WithMessage("Description must be between 1 and 500 characters.");
    }

    public override ValidationResult Validate(ValidationContext<UpdateCourseCommand> context)
    {
        return context.InstanceToValidate.Course is null
            ? new ValidationResult(new[] {
                new ValidationFailure("CourseForUpdateDto", "CourseForUpdateDto object is null")
            }) : base.Validate(context);
    }

}

