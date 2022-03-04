using Ardalis.GuardClauses;
using CU.SharedKernel.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class Instructor : EntityBaseT<int>
    {
        private Instructor()
        {

        }

        public Instructor(string lastName, string firstMidName, DateTime hireDate)
        {
            Guard.Against.NullOrWhiteSpace(lastName, nameof(lastName));
            Guard.Against.NullOrWhiteSpace(firstMidName, nameof(firstMidName));
            Guard.Against.OutOfSQLDateRange(hireDate, nameof(hireDate));
            LastName = lastName;
            FirstMidName = firstMidName;
            HireDate = hireDate;
        }

        public int ID { get; set; }

        [NotMapped]
        public override int Id { get { return ID; } }

        [Display(Name = "Last Name"), StringLength(50, MinimumLength = 1)]
        public string LastName { get; set; }

        [Column("FirstName"), Display(Name = "First Name"), StringLength(50, MinimumLength = 1)]
        public string FirstMidName { get; set; }

        [DataType(DataType.Date), Display(Name = "Hire Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime HireDate { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return LastName + ", " + FirstMidName; }
        }

        public virtual ICollection<Course> Courses { get; set; }
        public virtual OfficeAssignment OfficeAssignment { get; set; }
    }
}
