using CU.Application.Shared.ViewModels.Students;
using MediatR;

namespace CU.Application.Shared.DataRequests.SchoolItems.Queries
{
    public class GetStudentEditDtoQuery : IRequest<StudentEditDto>
    {
        public int StudentId { get; set; }
    }
}
