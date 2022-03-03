using System.Collections.Generic;

namespace CU.Application.Shared.ViewModels.Students
{
    public class StudentsListViewModel : StudentsViewModel
    {
        public IEnumerable<StudentListItem> StudentsList { get; set; } = new List<StudentListItem>();
    }
}
