using FluentValidation;

namespace AttendanceTracker.Models.DTO.Validators
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ClassSessionDtoValidator : AbstractValidator<ClassSessionDto>
    {
        public ClassSessionDtoValidator()
        {
            RuleFor(x => x.StudentClassId)
                .GreaterThan(0);

            RuleForEach(x => x.StudentIds)
                .GreaterThan(0);
        }
    }
}