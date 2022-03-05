using Ardalis.GuardClauses;
using CU.Application.Data.Common.Interfaces;
using CU.Application.Shared.DataRequests.SchoolItems.Commands;
using MediatR;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CASE = CU.Application.Shared.Common.Exceptions;
using CM = ContosoUniversity.Models;

namespace CU.Application.SchoolItems.Students.Commands.UpdateStudent
{
    public class UpdateStudentItemCommandHandler :  IRequestHandler<UpdateStudentItemCommand, int>
    {
        ISchoolDbContext Context { get; }

        public UpdateStudentItemCommandHandler(ISchoolDbContext context)
        {
            Guard.Against.Null(context, nameof(context));
            Context = context;
        }

        public async Task<int> Handle(UpdateStudentItemCommand request, CancellationToken cancellationToken)
        {
            CM.Student entity = await Context.Students.Where(s => s.ID == request.StudentId).SingleOrDefaultAsync();

            if (entity == null)
            {
                throw new CASE.NotFoundException(nameof(CM.Student), request.StudentId);
            }

            entity.EnrollmentDate = request.EnrollmentDate;
            entity.FirstMidName = request.FirstMidName;
            entity.LastName = request.LastName;

            await Context.SaveChangesAsync(cancellationToken);
            return entity.ID;
        }

    }
}
