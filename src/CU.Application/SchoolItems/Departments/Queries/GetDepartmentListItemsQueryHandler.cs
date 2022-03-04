using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using CU.Application.Data.Common.Interfaces;
using CU.Application.Shared.DataRequests.SchoolItems.Queries;
using CU.Application.Shared.Models.SchoolDtos;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace CU.Application.SchoolItems.Departments.Queries
{
    public class GetDepartmentListItemsQueryHandler : IRequestHandler<GetDepartmentListItemsQuery, List<DepartmentListItemDto>>
    {
        ISchoolDbContext Context { get; }
        IMapper Mapper { get; }

        public GetDepartmentListItemsQueryHandler(ISchoolDbContext context, IMapper mapper)
        {
            Guard.Against.Null(context, nameof(context));
            Guard.Against.Null(mapper, nameof(mapper));
            Context = context;
            Mapper = mapper;
        }

        public async Task<List<DepartmentListItemDto>> Handle(GetDepartmentListItemsQuery request, CancellationToken cancellationToken)
        {
            List<DepartmentListItemDto> departments = await Context.Departments
                .Include(d => d.Administrator)
                .OrderBy(d => d.Name)
                .ProjectTo<DepartmentListItemDto>(Mapper.ConfigurationProvider)
                .ToListAsync();
            return departments;
        }
    }
}
