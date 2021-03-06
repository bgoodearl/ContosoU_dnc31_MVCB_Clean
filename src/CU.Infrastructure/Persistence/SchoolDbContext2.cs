using ContosoUniversity.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CU.Infrastructure.Persistence
{
    public partial class SchoolDbContext //SchoolDbContext2.cs
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Course>()
                 .HasMany(c => c.Instructors).WithMany(i => i.Courses)
                 .Map(t => t.MapLeftKey("CourseID")
                     .MapRightKey("InstructorID")
                     .ToTable("CourseInstructor"));

            modelBuilder.Entity<Instructor>()
                .HasOptional(i => i.OfficeAssignment)
                .WithRequired(o => o.Instructor);

            modelBuilder.Entity<OfficeAssignment>()
                .HasKey(e => e.InstructorID)
                .ToTable("OfficeAssignment")
                .HasRequired(oa => oa.Instructor).WithOptional(i => i.OfficeAssignment);

        }
    }
}
