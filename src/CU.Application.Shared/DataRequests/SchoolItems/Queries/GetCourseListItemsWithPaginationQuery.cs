using MediatR;
using CU.Application.Shared.Common.Interfaces;
using CU.Application.Shared.Common.Models;
using CU.Application.Shared.Models.SchoolDtos;

namespace CU.Application.Shared.DataRequests.SchoolItems.Queries
{
    public class GetCourseListItemsWithPaginationQuery : IPaginatedListQuery, IRequest<PaginatedList<CourseListItemDto>>
    {
        /// <summary>
        /// InstructorID - optional - limits results to courses taught by instructor
        /// </summary>
        public int? InstructorID { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public CourseSortOrder SortOrder { get; set; }
    }
}
