using Ardalis.GuardClauses;
using CU.SharedKernel.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class Department : EntityBaseT<int>
    {
        private Department()
        {
            RowVersion = new byte[] { 0, 0, 0, 0 };
        }

        public Department(string name, decimal budget, DateTime startDate)
        {
            Guard.Against.NullOrWhiteSpace(name, nameof(name));
            Guard.Against.OutOfSQLDateRange(startDate, nameof(startDate));
            Name = name;
            Budget = budget;
            RowVersion = new byte[] { 0, 0, 0, 0 };
            StartDate = startDate;
        }

        public int DepartmentID { get; set; }

        [NotMapped]
        public override int Id { get { return DepartmentID; } }

        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Budget { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        public int? InstructorID { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public virtual Instructor Administrator { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
