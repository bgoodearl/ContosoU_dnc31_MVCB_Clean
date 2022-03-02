using ContosoUniversity.Models;
using CU.Application.Shared.ViewModels;
using CU.Application.Shared.ViewModels.Courses;
using CU.Application.Shared.ViewModels.Departments;
using CU.Application.Shared.ViewModels.Instructors;
using CU.Application.Shared.ViewModels.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CM = ContosoUniversity.Models;

namespace CU.Application.Common.Interfaces
{
    public interface ISchoolRepository : IDisposable
    {
        Task<CourseActionResult> AddNewCourseAsync(CourseEditDto course);
        Task<CourseEditDto> GetCourseEditDtoNoTrackingAsync(int courseID);
        Task<List<IdItem>> GetCourseInstructorsNoTrackingAsync(int courseID);
        Task<CourseListItem> GetCourseListItemNoTrackingAsync(int courseID);
        Task<List<CourseListItem>> GetCourseListItemsNoTrackingAsync();
        IQueryable<Course> GetCoursesQueryable();
        Task<List<DepartmentListItem>> GetDepartmentListItemsNoTrackingAsync();
        IQueryable<Department> GetDepartmentsQueryable();
        Task<List<InstructorListItem>> GetInstructorListItemsNoTrackingAsync();
        Task<List<StudentListItem>> GetStudentListItemsNoTrackingAsync();
        CM.Course RemoveCourse(CM.Course course);
        Task<CourseActionResult> SaveCourseChangesAsync(CourseEditDto course);
        Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken);
    }
}
