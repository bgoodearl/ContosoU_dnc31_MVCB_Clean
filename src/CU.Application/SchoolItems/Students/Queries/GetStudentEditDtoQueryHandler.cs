using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using CU.Application.Data.Common.Interfaces;
using CU.Application.Shared.DataRequests.SchoolItems.Queries;
using CU.Application.Shared.ViewModels.Students;
using AutoMapper.QueryableExtensions;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using CASE = CU.Application.Shared.Common.Exceptions;
using CM = ContosoUniversity.Models;

namespace CU.Application.SchoolItems.Students.Queries
{
    public class GetStudentEditDtoQueryHandler : IRequestHandler<GetStudentEditDtoQuery, StudentEditDto>
    {
        ISchoolDbContext Context { get; }
        IMapper Mapper { get; }

        public GetStudentEditDtoQueryHandler(ISchoolDbContext context, IMapper mapper)
        {
            Guard.Against.Null(context, nameof(context));
            Guard.Against.Null(mapper, nameof(mapper));
            Context = context;
            Mapper = mapper;
        }

        public async Task<StudentEditDto> Handle(GetStudentEditDtoQuery request, CancellationToken cancellationToken)
        {
            StudentEditDto studentEditDto = await Context.Students
                .Where(s => s.ID == request.StudentId)
                .ProjectTo<StudentEditDto>(Mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();

            if (studentEditDto == null)
            {
                throw new CASE.NotFoundException(nameof(CM.Student), request.StudentId);
            }
            return studentEditDto;
        }
    }
}
