using System.Collections.Generic;
using System.Threading.Tasks;
using CU.Application.Common.Interfaces;
using CU.Application.Shared.DataRequests.SchoolItems.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CASVMS = CU.Application.Shared.ViewModels.Students;

namespace ContosoUniversity.Controllers
{
    [Route("[Controller]/[Action]")]
    public partial class StudentsController : CUControllerBase
    {
        public StudentsController(IHttpContextAccessor httpContextAccessor)
            : base(httpContextAccessor)
        {
        }

        [Route("~/[Controller]/{mode?}/{id?}")]
        public async Task<IActionResult> Index(int? mode, int? id)
        {
            CASVMS.StudentsListViewModel model = new CASVMS.StudentsListViewModel
            {
                ItemID = id,
                ViewMode = mode.HasValue ? mode.Value : 0
            };
            if (mode < 0)
            {
                GetStudentListItemsQuery query = new GetStudentListItemsQuery();
                model.StudentsList = await SendQueryAsync(query);
            }
            return View(model);
        }
    }
}
