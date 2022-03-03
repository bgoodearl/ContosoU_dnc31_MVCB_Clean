using CU.Application.Shared.ViewModels.Instructors;
using MediatR;
using System.Collections.Generic;

namespace CU.Application.Shared.DataRequests.SchoolItems.Queries
{
    public class GetInstructorListItemsQuery : IRequest<List<InstructorListItem>>
    {
    }
}
