using Ardalis.GuardClauses;
using CU.Application.Common.Interfaces;
using CU.Application.Shared.Interfaces;
using CU.Application.Shared.ViewModels;
using CU.Application.Shared.ViewModels.Courses;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CM = ContosoUniversity.Models;

namespace CU.Infrastructure.Repositories
{
    public class SchoolViewDataRepository : ISchoolViewDataRepository
    {
        public SchoolViewDataRepository(ISchoolRepositoryFactory schoolRepositoryFactory)
        {
            Guard.Against.Null(schoolRepositoryFactory, nameof(schoolRepositoryFactory));
            SchoolRepositoryFactory = schoolRepositoryFactory;
        }

        protected ISchoolRepositoryFactory SchoolRepositoryFactory { get; }

        #region ISchoolViewDataRepository

        public async Task<CourseActionResult> DeleteCourseAsync(int courseID)
        {
            using (ISchoolRepository repo = SchoolRepositoryFactory.GetSchoolRepository())
            {
                CourseActionResult result = new CourseActionResult
                {
                    Action = "DeleteCourse",
                    CourseID = courseID
                };
                CM.Course course = await repo.GetCoursesQueryable()
                    .Where(x => x.CourseID == courseID)
                    .SingleOrDefaultAsync();
                if (course == null)
                {
                    result.ErrorMessage = $"Course {courseID} not found";
                }
                else
                {
#if DEBUG
                    CM.Course courseRemoved =
#endif
                    repo.RemoveCourse(course);
                    result.ChangeCount = await repo.SaveChangesAsync(new System.Threading.CancellationToken());
                }

                return result;
            }
        }

        public async Task<CourseEditDto> GetCourse2EditAsync(int courseID)
        {
            using (ISchoolRepository repo = SchoolRepositoryFactory.GetSchoolRepository())
            {
                return await repo.GetCourseEditDtoNoTrackingAsync(courseID);
            }
        }

        public async Task<CourseItem> GetCourseDetailsNoTrackingAsync(int courseID)
        {
            using (ISchoolRepository repo = SchoolRepositoryFactory.GetSchoolRepository())
            {
                CourseItem course = new CourseItem(await repo.GetCourseListItemNoTrackingAsync(courseID));
                if (course != null)
                {
                    course.SetInstructors(await repo.GetCourseInstructorsNoTrackingAsync(courseID));
                }
                return course;
            }
        }

        public async Task<IList<CourseListItem>> GetCourseListNoTrackingAsync()
        {
            using (ISchoolRepository repo = SchoolRepositoryFactory.GetSchoolRepository())
            {
                return await repo.GetCourseListItemsNoTrackingAsync();
            }
        }

        public async Task<List<IdItem>> GetDepartmentsListAsync()
        {
            using (ISchoolRepository repo = SchoolRepositoryFactory.GetSchoolRepository())
            {
                List<IdItem> idItems = await repo.GetDepartmentsQueryable()
                    .AsNoTracking()
                    .OrderBy(d => d.Name)
                    .Select(d => new IdItem
                    {
                        Id = d.DepartmentID,
                        Name = d.Name
                    })
                    .ToListAsync();
                return idItems;
            }
        }

        public async Task<CourseActionResult> SaveCourseEditChangesAsync(int courseID, CourseEditDto model)
        {
            using (ISchoolRepository repo = SchoolRepositoryFactory.GetSchoolRepository())
            {
                return await repo.SaveCourseChangesAsync(model);
            }
        }

        public async Task<CourseActionResult> SaveNewCourseAsync(CourseEditDto model)
        {
            using (ISchoolRepository repo = SchoolRepositoryFactory.GetSchoolRepository())
            {
                int departmentID;
                if (!string.IsNullOrWhiteSpace(model.DepartmentIDstr) && int.TryParse(model.DepartmentIDstr, out departmentID))
                {
                    model.DepartmentID = departmentID;
                }
                if (model.DepartmentID == 0)
                {
                    CourseActionResult result = new CourseActionResult
                    {
                        Action = "SaveNewCourseAsync",
                        ErrorMessage = "DepartmentID is required"
                    };
                    return result;
                }
                else
                {
                    CourseActionResult result = await repo.AddNewCourseAsync(model);
                    return result;
                }
            }
        }

        #endregion ISchoolViewDataRepository

    }
}
