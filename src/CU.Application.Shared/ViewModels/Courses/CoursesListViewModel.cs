
using System.Collections.Generic;

namespace CU.Application.Shared.ViewModels.Courses
{
    public class CoursesListViewModel : SchoolItemViewModel
    {
        public IEnumerable<CourseListItem> CourseList { get; set; } = new List<CourseListItem>();
    }
}
