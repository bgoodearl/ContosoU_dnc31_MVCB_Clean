using System.Collections.Generic;
using System.Threading.Tasks;
using CU.Application.Common.Interfaces;
using CU.Application.Shared.DataRequests.SchoolItems.Queries;
using CU.Application.Shared.Models.SchoolDtos;
using CU.Application.Shared.ViewModels.Departments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContosoUniversity.Controllers
{
    [Route("[Controller]/[Action]")]
    public class DepartmentsController : CUControllerBase
    {
        public DepartmentsController(IHttpContextAccessor httpContextAccessor)
            : base(httpContextAccessor)
        {
        }

        [Route("~/[Controller]")]
        public async Task<IActionResult> Index()
        {
#if true
            GetDepartmentListItemsQuery query = new GetDepartmentListItemsQuery();
            IList<DepartmentListItemDto> queryResult = await SendQueryAsync(query);
            return View(queryResult);
#else
            using (ISchoolRepository repo = GetSchoolRepository())
            {
                List<DepartmentListItem> departments = await repo.GetDepartmentListItemsNoTrackingAsync();
                return View(departments);
            }
#endif
        }

    }
}
