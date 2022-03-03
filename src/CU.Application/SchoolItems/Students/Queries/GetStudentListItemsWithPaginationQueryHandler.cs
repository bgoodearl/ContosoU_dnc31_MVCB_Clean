using Ardalis.GuardClauses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using CU.Application.Common.Interfaces;
using CU.Application.Common.Mapping;
using CU.Application.Shared.Common.Models;
using CU.Application.Shared.DataRequests.SchoolItems.Queries;
using CU.Application.Shared.ViewModels.Students;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace CU.Application.SchoolItems.Students.Queries
{
    public class GetStudentListItemsWithPaginationQueryHandler : IRequestHandler<GetStudentListItemsWithPaginationQuery, PaginatedList<StudentListItem>>
    {
        ISchoolDbContext Context { get; }
        IMapper Mapper { get; }

        public GetStudentListItemsWithPaginationQueryHandler(ISchoolDbContext context, IMapper mapper)
        {
            Guard.Against.Null(context, nameof(context));
            Guard.Against.Null(mapper, nameof(mapper));
            Context = context;
            Mapper = mapper;
        }

        public async Task<PaginatedList<StudentListItem>> Handle(GetStudentListItemsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await Context.Students
                .OrderBy(d => d.LastName)
                .ThenBy(i => i.FirstMidName)
                .ProjectTo<StudentListItem>(Mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
