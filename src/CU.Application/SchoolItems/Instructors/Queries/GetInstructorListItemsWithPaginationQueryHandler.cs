using Ardalis.GuardClauses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using CU.Application.Data.Common.Interfaces;
using CU.Application.Shared.DataRequests.SchoolItems.Queries;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using CU.Application.Shared.ViewModels.Instructors;
using CU.Application.Shared.Common.Models;
using CU.Application.Common.Mapping;

namespace CU.Application.SchoolItems.Instructors.Queries
{
    public class GetInstructorListItemsWithPaginationQueryHandler : IRequestHandler<GetInstructorListItemsWithPaginationQuery, PaginatedList<InstructorListItem>>
    {
        ISchoolDbContext Context { get; }
        IMapper Mapper { get; }

        public GetInstructorListItemsWithPaginationQueryHandler(ISchoolDbContext context, IMapper mapper)
        {
            Guard.Against.Null(context, nameof(context));
            Guard.Against.Null(mapper, nameof(mapper));
            Context = context;
            Mapper = mapper;
        }

        public async Task<PaginatedList<InstructorListItem>> Handle(GetInstructorListItemsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await Context.Instructors
                .OrderBy(d => d.LastName)
                .ThenBy(i => i.FirstMidName)
                .ProjectTo<InstructorListItem>(Mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
