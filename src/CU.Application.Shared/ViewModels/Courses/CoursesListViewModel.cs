
using System.Collections.Generic;

namespace CU.Application.Shared.ViewModels.Courses
{
    public class CoursesListViewModel : CoursesViewModel
    {
        public IEnumerable<CourseListItem> CourseList { get; set; } = new List<CourseListItem>();
    }
}
