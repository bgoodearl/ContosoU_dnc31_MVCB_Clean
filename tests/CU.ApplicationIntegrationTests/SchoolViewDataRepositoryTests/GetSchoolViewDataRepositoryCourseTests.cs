using System.Collections.Generic;
using System.Threading.Tasks;
using CU.Application.Shared.Interfaces;
using CU.Application.Shared.ViewModels.Courses;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace CU.ApplicationIntegrationTests.SchoolViewDataRepositoryTests
{
    [Collection(TestFixture.DbCollectionName)]
    public class GetSchoolViewDataRepositoryCourseTests : DbContextTestBase
    {
        public GetSchoolViewDataRepositoryCourseTests(ITestOutputHelper testOutputHelper, TestFixture fixture)
            : base(testOutputHelper, fixture)
        {
        }

        [Fact]
        public async Task CanGetCourseListNoTrackingAsync()
        {
            ISchoolViewDataRepository repo = _fixture.GetSchoolViewDataRepositoryFactory(_testOutputHelper).GetViewDataRepository();
            IList<CourseListItem> courseListItems = await repo.GetCourseListNoTrackingAsync();
            courseListItems.Should().NotBeNullOrEmpty();
            CourseListItem firstCourse = courseListItems[0];
            firstCourse.Should().NotBeNull();
            _testOutputHelper.WriteLine($"Have {courseListItems.Count} courses - first course ({firstCourse.CourseID}) [{firstCourse.Title}]");
        }
    }
}
