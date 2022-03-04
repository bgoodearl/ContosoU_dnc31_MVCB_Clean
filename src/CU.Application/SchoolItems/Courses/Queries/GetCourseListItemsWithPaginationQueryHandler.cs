using Ardalis.GuardClauses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using CU.Application.Data.Common.Interfaces;
using CU.Application.Common.Mapping;
using CU.Application.Shared.Common.Models;
using CU.Application.Shared.DataRequests.SchoolItems.Queries;
using CU.Application.Shared.Models.SchoolDtos;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace CU.Application.SchoolItems.Courses.Queries
{
    public class GetCourseListItemsWithPaginationQueryHandler : IRequestHandler<GetCourseListItemsWithPaginationQuery, PaginatedList<CourseListItemDto>>
    {
        ISchoolDbContext Context { get; }
        IMapper Mapper { get; }

        public GetCourseListItemsWithPaginationQueryHandler(ISchoolDbContext context, IMapper mapper)
        {
            Guard.Against.Null(context, nameof(context));
            Guard.Against.Null(mapper, nameof(mapper));
            Context = context;
            Mapper = mapper;
        }

        public async Task<PaginatedList<CourseListItemDto>> Handle(GetCourseListItemsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            if (request.SortOrder == CourseSortOrder.ByCourseTitle)
            {
                return await Context.Courses
                    .Include(c => c.Department)
                    .OrderBy(c => c.Title)
                    .ThenBy(c => c.CourseID)
                    .ProjectTo<CourseListItemDto>(Mapper.ConfigurationProvider)
                    .PaginatedListAsync(request.PageNumber, request.PageSize);
            }
            else //request.SortOrder == CourseSortOrder.ByCourseID
            {
                return await Context.Courses
                    .Include(c => c.Department)
                    .OrderBy(c => c.CourseID)
                    .ProjectTo<CourseListItemDto>(Mapper.ConfigurationProvider)
                    .PaginatedListAsync(request.PageNumber, request.PageSize);
            }
        }
    }
}
