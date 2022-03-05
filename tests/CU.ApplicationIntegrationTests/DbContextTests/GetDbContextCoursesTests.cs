using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CU.Application.Data.Common.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;
using CM = ContosoUniversity.Models;

namespace CU.ApplicationIntegrationTests.DbContextTests
{
    [Collection(TestFixture.DbCollectionName)]
    public class GetDbContextCoursesTests : DbContextTestBase
    {
        public GetDbContextCoursesTests(ITestOutputHelper testOutputHelper, TestFixture fixture)
            : base(testOutputHelper, fixture)
        {

        }

        [Fact]
        public async Task CanGetCoursesAsync()
        {
            int maxCourseCount = 5;

            using (var scope = _fixture.GetServiceScopeFactory(_testOutputHelper).CreateScope())
            {
                ISchoolDbContext cuContext = scope.ServiceProvider.GetRequiredService<ISchoolDbContext>();
                cuContext.Should().NotBeNull();

                List<CM.Course> courses = await cuContext.Courses
                    .Take(maxCourseCount).ToListAsync();
                courses.Should().NotBeNullOrEmpty();
                courses.Count.Should().BePositive();
                _testOutputHelper.WriteLine($"courses.Count = {courses.Count}");
            }
        }

        [Fact]
        public async Task CanGetCoursesWithInstructorsAsync()
        {
            int maxCourseCount = 5;

            using (var scope = _fixture.GetServiceScopeFactory(_testOutputHelper).CreateScope())
            {
                ISchoolDbContext cuContext = scope.ServiceProvider.GetRequiredService<ISchoolDbContext>();
                cuContext.Should().NotBeNull();

                List<CM.Course> courses = await cuContext.Courses
                    .Include(c => c.Instructors)
                    .Take(maxCourseCount).ToListAsync();
                courses.Should().NotBeNullOrEmpty();
                courses.Count.Should().BePositive();
                CM.Course firstCourse = courses.First();
                int coursesWithInstructorsCount = 0;
                foreach(var c in courses)
                {
                    if (c.Instructors.Count > 0)
                        coursesWithInstructorsCount++;
                }
                _testOutputHelper.WriteLine($"courses.Count = {courses.Count}, with Instructors count = {coursesWithInstructorsCount}");
                firstCourse.Should().NotBeNull();
                string? firstCourseFirstInstructorName = firstCourse.Instructors.Count > 0 ? firstCourse.Instructors.First().FullName : null;
                _testOutputHelper.WriteLine($"First Course ID={firstCourse.CourseID} (Id={firstCourse.Id}) [{firstCourse.Title}], # Instructors: {firstCourse.Instructors.Count} first=[{firstCourseFirstInstructorName}]");
                coursesWithInstructorsCount.Should().BePositive();
            }
        }
    }
}
