using CU.Application.Data.Common.Interfaces;
using CU.Application.Shared.DataRequests.SchoolItems.Queries;
using CU.Application.Shared.Models.SchoolDtos;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace CU.ApplicationIntegrationTests.ApplicationTests
{
    [Collection(TestFixture.DbCollectionName)]
    public class GetCoursesTests : ApplicationTestBase
    {
        public GetCoursesTests(ITestOutputHelper testOutputHelper, TestFixture fixture)
            : base(testOutputHelper, fixture)
        {

        }

        [Fact]
        public async Task CanGetCoursesPaginated()
        {
            GetCourseListItemsWithPaginationQuery query = new GetCourseListItemsWithPaginationQuery
            {
                PageNumber = 1,
                PageSize = 5,
                SortOrder = CourseSortOrder.ByCourseID
            };

            var result = await SendAsync(query);
            result.Should().NotBeNull();
            result.Items.Should().NotBeNull();
            result.Items.Count.Should().BePositive();
            result.TotalCount.Should().BeGreaterThanOrEqualTo(result.Items.Count);
            CourseListItemDto firstListItem = result.Items.First();
            firstListItem.Should().NotBeNull();

            _testOutputHelper.WriteLine($"First course CourseID = {firstListItem.CourseID}, Title = [{firstListItem.Title}]");
            _testOutputHelper.WriteLine($"Items.Count = {result.Items.Count}, TotalCount = {result.TotalCount}");
        }

        [SkippableFact]
        public async Task CanGetCoursesPaginatedForInstructor()
        {
            int instructorId = 0;
            const string instructorLastName = "Kapoor";
            using (var scope = _fixture.GetServiceScopeFactory(_testOutputHelper).CreateScope())
            {
                ISchoolDbContext cuContext = scope.ServiceProvider.GetRequiredService<ISchoolDbContext>();
                cuContext.Should().NotBeNull();
                instructorId = cuContext.Instructors.Where(i => i.LastName == instructorLastName)
                    .Select(i => i.ID)
                    .SingleOrDefault();
                Skip.If(instructorId == 0, $"Did not find Instructor with Last Name [{instructorLastName}] - test cannot be completed");
            }

            GetCourseListItemsWithPaginationQuery query = new GetCourseListItemsWithPaginationQuery
            {
                InstructorID = instructorId,
                PageNumber = 1,
                PageSize = 5,
                SortOrder = CourseSortOrder.ByCourseID
            };

            var result = await SendAsync(query);
            result.Should().NotBeNull();
            result.Items.Should().NotBeNull();
            result.Items.Count.Should().BePositive();
            result.TotalCount.Should().BeGreaterThanOrEqualTo(result.Items.Count);
            CourseListItemDto firstListItem = result.Items.First();
            firstListItem.Should().NotBeNull();
            _testOutputHelper.WriteLine($"First course for instructor [{instructorLastName}], CourseID = {firstListItem.CourseID}, Title = [{firstListItem.Title}]");
            _testOutputHelper.WriteLine($"Items.Count = {result.Items.Count}, TotalCount = {result.TotalCount}");
        }
    }
}
