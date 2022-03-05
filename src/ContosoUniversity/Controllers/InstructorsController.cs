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

        [Route("~/[Controller]/{mode?}/{id?}")]
        public async Task<IActionResult> Index(int? mode, int? id)
        {
            InstructorsListViewModel model = new InstructorsListViewModel
            {
                ItemID = id,
                ViewMode = mode.HasValue ? mode.Value : 0
            };
            if (mode < 0)
            {
                GetInstructorListItemsQuery query = new GetInstructorListItemsQuery();
                model.Instructors = await SendQueryAsync(query);
            }
            return View(model);
        }
    }
}
