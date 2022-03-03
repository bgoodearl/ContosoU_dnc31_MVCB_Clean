using System.Collections.Generic;
using System.Threading.Tasks;
//using CU.Application.Common.Interfaces;
using CU.Application.Shared.DataRequests.SchoolItems.Queries;
using CU.Application.Shared.ViewModels.Instructors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContosoUniversity.Controllers
{
    [Route("[Controller]/[Action]")]
    public class InstructorsController : CUControllerBase
    {
        public InstructorsController(IHttpContextAccessor httpContextAccessor)
            : base(httpContextAccessor)
        {
        }

        [Route("~/[Controller]")]
        public async Task<IActionResult> Index()
        {
#if true
            GetInstructorListItemsQuery query = new GetInstructorListItemsQuery();
            List<InstructorListItem> instructors = await SendQueryAsync(query);
            return View(instructors);
#else
            using (ISchoolRepository repo = GetSchoolRepository())
            {
                List<InstructorListItem> instructors = await repo.GetInstructorListItemsNoTrackingAsync();
                return View(instructors);
            }
#endif
        }
    }
}
