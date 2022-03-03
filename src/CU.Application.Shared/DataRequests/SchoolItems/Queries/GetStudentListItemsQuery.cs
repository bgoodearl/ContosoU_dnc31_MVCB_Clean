using CU.Application.Shared.ViewModels.Students;
using MediatR;
using System.Collections.Generic;

namespace CU.Application.Shared.DataRequests.SchoolItems.Queries
{
    public class GetStudentListItemsQuery : IRequest<List<StudentListItem>>
    {
    }
}
