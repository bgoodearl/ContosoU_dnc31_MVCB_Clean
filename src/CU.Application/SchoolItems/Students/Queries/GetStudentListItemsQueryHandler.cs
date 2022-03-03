using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using CU.Application.Common.Interfaces;
using CU.Application.Shared.DataRequests.SchoolItems.Queries;
using CU.Application.Shared.ViewModels.Students;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace CU.Application.SchoolItems.Students.Queries
{
    public class GetStudentListItemsQueryHandler : IRequestHandler<GetStudentListItemsQuery, List<StudentListItem>>
    {
        ISchoolDbContext Context { get; }
        IMapper Mapper { get; }

        public GetStudentListItemsQueryHandler(ISchoolDbContext context, IMapper mapper)
        {
            Guard.Against.Null(context, nameof(context));
            Guard.Against.Null(mapper, nameof(mapper));
            Context = context;
            Mapper = mapper;
        }

        public async Task<List<StudentListItem>> Handle(GetStudentListItemsQuery request, CancellationToken cancellationToken)
        {
            List<StudentListItem> students = await Context.Students
                .OrderBy(d => d.LastName)
                .ThenBy(i => i.FirstMidName)
                .ProjectTo<StudentListItem>(Mapper.ConfigurationProvider)
                .ToListAsync();
            return students;
        }
    }
}
