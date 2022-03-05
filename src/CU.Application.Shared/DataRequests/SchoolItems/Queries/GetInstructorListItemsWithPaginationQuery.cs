using MediatR;
using CU.Application.Shared.ViewModels.Instructors;
using CU.Application.Shared.Common.Interfaces;
using CU.Application.Shared.Common.Models;

namespace CU.Application.Shared.DataRequests.SchoolItems.Queries
{
    public class GetInstructorListItemsWithPaginationQuery : IPaginatedListQuery, IRequest<PaginatedList<InstructorListItem>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
