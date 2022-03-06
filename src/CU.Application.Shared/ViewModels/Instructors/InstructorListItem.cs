using System;
using System.ComponentModel.DataAnnotations;

namespace CU.Application.Shared.ViewModels.Instructors
{
    public class InstructorListItem
    {
        public int ID { get; set; }
        public string FirstMidName { get; set; } = string.Empty;

        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return LastName + ", " + FirstMidName; }
        }

        public DateTime HireDate { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string OfficeAssignment { get; set; } = string.Empty;
    }
}
