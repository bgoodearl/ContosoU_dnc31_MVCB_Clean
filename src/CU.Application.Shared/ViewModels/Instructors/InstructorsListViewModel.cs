using System.Collections.Generic;

namespace CU.Application.Shared.ViewModels.Instructors
{
    public class InstructorsListViewModel : SchoolItemViewModel
    {
        public List<InstructorListItem> Instructors { get; set; } = new List<InstructorListItem>();
    }
}
