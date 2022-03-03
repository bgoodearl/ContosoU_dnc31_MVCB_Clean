using System.Collections.Generic;
using System.Threading.Tasks;
using CU.Application.Common.Interfaces;
using CU.Application.Shared.ViewModels.Courses;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace CU.ApplicationIntegrationTests.SchoolRepositoryTests
{
    [Collection(TestFixture.DbCollectionName)]
    public class GetSchoolRepositoryCoursesTests : DbContextTestBase
    {
        public GetSchoolRepositoryCoursesTests(ITestOutputHelper testOutputHelper, TestFixture fixture)
            : base(testOutputHelper, fixture)
        {
        }

        [Fact]
        public async Task CanGetCourseListAsync()
        {
            using (ISchoolRepository repo = _fixture.GetSchoolRepositoryFactory(_testOutputHelper).GetSchoolRepository())
            {
                List<CourseListItem> courseList = await repo.GetCourseListItemsNoTrackingAsync();
                courseList.Should().NotBeNullOrEmpty();
                CourseListItem firstCourse = courseList[0];
                firstCourse.Should().NotBeNull();
                _testOutputHelper.WriteLine($"Have {courseList.Count} courses - first course ({firstCourse.CourseID}) [{firstCourse.Title}]");
            }
        }
    }
}
