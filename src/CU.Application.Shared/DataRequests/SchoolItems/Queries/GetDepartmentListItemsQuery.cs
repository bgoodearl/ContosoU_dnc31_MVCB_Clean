using CU.Application.Shared.Models.SchoolDtos;
using MediatR;
using System.Collections.Generic;

namespace CU.Application.Shared.DataRequests.SchoolItems.Queries
{
    public class GetDepartmentListItemsQuery : IRequest<List<DepartmentListItemDto>>
    {
    }
}
