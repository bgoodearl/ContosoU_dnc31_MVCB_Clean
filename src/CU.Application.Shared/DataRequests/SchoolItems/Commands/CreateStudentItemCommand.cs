using MediatR;
using System;

namespace CU.Application.Shared.DataRequests.SchoolItems.Commands
{
    public class CreateStudentItemCommand : IRequest<int>
    {
        public DateTime EnrollmentDate { get; set; }
        public string FirstMidName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
