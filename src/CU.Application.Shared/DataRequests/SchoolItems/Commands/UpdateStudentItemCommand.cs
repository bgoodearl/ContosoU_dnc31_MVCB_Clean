using CU.Application.Shared.ViewModels.Students;
using MediatR;
using System;

namespace CU.Application.Shared.DataRequests.SchoolItems.Commands
{
    public class UpdateStudentItemCommand : IRequest<int>
    {
        public UpdateStudentItemCommand()
        {
        }

        public UpdateStudentItemCommand(StudentEditDto dto)
        {
            EnrollmentDate = dto.EnrollmentDate;
            FirstMidName = dto.FirstMidName;
            LastName = dto.LastName;
            StudentId = dto.ID;
        }

        public DateTime EnrollmentDate { get; set; }
        public string FirstMidName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int StudentId { get; set; }
    }
}
