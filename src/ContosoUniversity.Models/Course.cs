using Ardalis.GuardClauses;
using ContosoUniversity.Models.Lookups;
using CU.SharedKernel.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class Course : EntityBaseT<int>
    {
        private Course()
        {
        }

        public Course(int courseID, string title, Department department)
        {
            Guard.Against.OutOfRange(courseID, nameof(courseID), 1, int.MaxValue);
            Guard.Against.NullOrWhiteSpace(title, nameof(title));
            Guard.Against.Null(department, nameof(department));
            Guard.Against.Zero(department.DepartmentID, nameof(department.DepartmentID));
            CourseID = courseID;
            Title = title;
            Department = department;
            DepartmentID = department.DepartmentID;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Number")]
        public int CourseID { get; set; }

        [NotMapped]
        public override int Id { get { return CourseID; } }

        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

        [Range(0, 5)]
        public int Credits { get; set; }

        public int DepartmentID { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public virtual ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();

        private ICollection<CoursePresentationType> _coursePresentationTypes;
        public virtual ICollection<CoursePresentationType> CoursePresentationTypes
        {
            get { return _coursePresentationTypes ?? (_coursePresentationTypes = new List<CoursePresentationType>()); }
            protected set { _coursePresentationTypes = value; }
        }

    }
}