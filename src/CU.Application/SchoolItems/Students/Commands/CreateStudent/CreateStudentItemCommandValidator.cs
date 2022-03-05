using CU.Application.Shared.DataRequests.SchoolItems.Commands;
using FluentValidation;

namespace CU.Application.SchoolItems.Students.Commands.CreateStudent
{
    public class CreateStudentItemCommandValidator : AbstractValidator<CreateStudentItemCommand>
    {

        public CreateStudentItemCommandValidator()
        {
            RuleFor(v => v.LastName)
                .MaximumLength(50)
                .NotEmpty();

            RuleFor(v => v.FirstMidName)
                .MaximumLength(50)
                .NotEmpty();

            RuleFor(v => v.EnrollmentDate)
                .GreaterThanOrEqualTo(StudentCommandConstants.Jan1_1900)
                .LessThan(StudentCommandConstants.Jan1_2300);
        }
    }
}
