using Ardalis.GuardClauses;
using ContosoUniversity.Models;
using CU.Application.Data.Common.Interfaces;
using CU.Application.Shared.DataRequests.SchoolItems.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CU.Application.SchoolItems.Students.Commands.CreateStudent
{
    public class CreateStudentItemCommandHandler : IRequestHandler<CreateStudentItemCommand, int>
    {
        ISchoolDbContext Context { get; }

        public CreateStudentItemCommandHandler(ISchoolDbContext context)
        {
            Guard.Against.Null(context, nameof(context));
            Context = context;
        }

        public async Task<int> Handle(CreateStudentItemCommand request, CancellationToken cancellationToken)
        {
            var entity = new Student(request.LastName, request.FirstMidName, request.EnrollmentDate);
            //TODO: when Domain Events are figured out: //entity.DomainEvents.Add(new StudentCreatedEvent(entity));
            Context.Students.Add(entity);
            await Context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
