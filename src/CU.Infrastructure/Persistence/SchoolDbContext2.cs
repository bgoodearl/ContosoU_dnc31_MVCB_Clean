using ContosoUniversity.Models;
using ContosoUniversity.Models.Lookups;
using CU.Definitions.Lookups;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CU.Infrastructure.Persistence
{
    public partial class SchoolDbContext //SchoolDbContext2.cs
    {
        private static class Tags
        {
            internal const string _SubType = "_SubType";
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Course>()
                 .HasMany(c => c.Instructors).WithMany(i => i.Courses)
                 .Map(t => t.MapLeftKey("CourseID")
                     .MapRightKey("InstructorID")
                     .ToTable("CourseInstructor"));

            modelBuilder.Entity<Course>()
                .HasMany(c => c.CoursePresentationTypes).WithMany(cpt => cpt.Courses)
                .Map(m =>
                {
                    m.MapLeftKey("CourseID");
                    m.MapRightKey("LookupTypeId", "CoursePresentationTypeCode");
                    m.ToTable("_coursesPresentationTypes");
                });

            modelBuilder.Entity<Department>()
                .HasMany(d => d.DepartmentFacilityTypes).WithMany(df => df.Departments)
                .Map(m =>
                {
                    m.MapLeftKey("DepartmentID");
                    m.MapRightKey("LookupTypeId", "DepartmentFacilityTypeCode");
                    m.ToTable("_departmentsFacilityTypes");
                });

            modelBuilder.Entity<Instructor>()
                .HasOptional(i => i.OfficeAssignment)
                .WithRequired(o => o.Instructor);

            modelBuilder.Entity<OfficeAssignment>()
                .HasKey(e => e.InstructorID)
                .ToTable("OfficeAssignment")
                .HasRequired(oa => oa.Instructor).WithOptional(i => i.OfficeAssignment);



            //*******************************************
            #region LookupBaseWith2cKey Subclass Mappings

            modelBuilder.Entity<LookupBaseWith2cKey>()
                .HasKey(l => new { l.LookupTypeId, l.Code })
                .ToTable("xLookups2cKey");

            modelBuilder.Entity<LookupBaseWith2cKey>()
                .Property(l => l.LookupTypeId)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new[]
                        {
                            new IndexAttribute("LookupTypeAndName", 1) {IsUnique=true}
                        }));
            modelBuilder.Entity<LookupBaseWith2cKey>()
                .Property(l => l.Name)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new[]
                        {
                            new IndexAttribute("LookupTypeAndName", 2) {IsUnique=true}
                        }));

            modelBuilder.Entity<LookupBaseWith2cKey>()
                .Map<CoursePresentationType>(bt => bt.Requires(Tags._SubType).HasValue((short)CULookupTypes.CoursePresentationType));

            modelBuilder.Entity<LookupBaseWith2cKey>()
                .Map<DepartmentFacilityType>(bt => bt.Requires(Tags._SubType).HasValue((short)CULookupTypes.DepartmentFacilityType));

            #endregion LookupBaseWith2cKey Subclass Mappings

        }
    }
}
