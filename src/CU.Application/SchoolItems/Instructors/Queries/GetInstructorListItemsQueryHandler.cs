using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using CU.Application.Common.Interfaces;
using CU.Application.Shared.DataRequests.SchoolItems.Queries;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using CU.Application.Shared.ViewModels.Instructors;

namespace CU.Application.SchoolItems.Instructors.Queries
{
    public class GetInstructorListItemsQueryHandler : IRequestHandler<GetInstructorListItemsQuery, List<InstructorListItem>>
    {
        ISchoolDbContext Context { get; }
        IMapper Mapper { get; }

        public GetInstructorListItemsQueryHandler(ISchoolDbContext context, IMapper mapper)
        {
            Guard.Against.Null(context, nameof(context));
            Guard.Against.Null(mapper, nameof(mapper));
            Context = context;
            Mapper = mapper;
        }

        public async Task<List<InstructorListItem>> Handle(GetInstructorListItemsQuery request, CancellationToken cancellationToken)
        {
            List<InstructorListItem> instructors = await Context.Instructors
                .Include(d => d.OfficeAssignment)
                .OrderBy(d => d.LastName)
                .ThenBy(i => i.FirstMidName)
                .ProjectTo<InstructorListItem>(Mapper.ConfigurationProvider)
                .ToListAsync();
            return instructors;
        }

    }
}
