using CU.Application.Shared.DataRequests.SchoolItems.Queries;
using CU.Application.Shared.Models.SchoolDtos;
using FluentAssertions;
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
    }
}
