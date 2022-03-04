using Ardalis.GuardClauses;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class OfficeAssignment
    {
        private OfficeAssignment()
        {

        }

        public OfficeAssignment(Instructor instructor, string location)
        {
            Guard.Against.Null(instructor, nameof(instructor));
            Guard.Against.Zero(instructor.ID, "instructor.ID");
            Guard.Against.NullOrWhiteSpace(location, nameof(location));
            Instructor = instructor;
            InstructorID = instructor.ID;
            Location = location;
        }

        [ForeignKey("Instructor")]
        public int InstructorID { get; set; }
        [StringLength(50)]
        [Display(Name = "Office Location")]
        public string Location { get; set; }

        public virtual Instructor Instructor { get; set; }
    }
}
