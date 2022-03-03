using CU.Application.Shared.ViewModels.Courses;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using static CU.Application.Shared.CommonDefs;

namespace ContosoUniversity.Components
{
    public partial class CourseDetails
    {
        [Parameter] public CourseItem Course { get; set; }
    }
}
