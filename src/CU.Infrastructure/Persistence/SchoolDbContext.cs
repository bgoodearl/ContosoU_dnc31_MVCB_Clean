using CU.Application.Data.Common.Interfaces;
using ContosoUniversity.Models;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using ContosoUniversity.Models.Lookups;
using System.Linq;

namespace CU.Infrastructure.Persistence
{
    public partial class SchoolDbContext : DbContext, ISchoolDbContext
    {
        public SchoolDbContext() : base("SchoolContext")
        {
        }

        public SchoolDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        #region Persistent Entities

        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        public DbSet<Student> Students { get; set; }

        #endregion Persistent Entities


        #region Lookups

        public DbSet<LookupBaseWith2cKey> LookupsWith2cKey { get; set; }
        public DbSet<LookupType> LookupTypes { get; set; }
        public DbSet<CoursePresentationType> CoursePresentationTypes { get; set; }
        public DbSet<DepartmentFacilityType> DepartmentFacilityTypes { get; set; }

        #endregion Lookups


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> SeedDataNeededAsync()
        {
            if ((await Students.CountAsync() == 0) || (await Instructors.CountAsync() == 0)
                        || (await Courses.CountAsync() == 0) || (await Enrollments.CountAsync() == 0)
                        || (await LookupTypes.CountAsync() == 0)
                        || (await CoursePresentationTypes.CountAsync() == 0)
                        || (await DepartmentFacilityTypes.CountAsync() == 0))
            {
                return true;
            }
            return false;
        }

        public async Task<int> SeedInitialDataAsync()
        {
            return await SchoolDbContextSeed.SeedInitialData(this);
        }

    }
}
