using MediatR;
using CU.Application.Shared.ViewModels.Students;
using CU.Application.Shared.Common.Interfaces;
using CU.Application.Shared.Common.Models;

namespace CU.Application.Shared.DataRequests.SchoolItems.Queries
{
    public class GetStudentListItemsWithPaginationQuery : IPaginatedListQuery, IRequest<PaginatedList<StudentListItem>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
