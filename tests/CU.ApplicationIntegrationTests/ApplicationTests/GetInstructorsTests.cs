using CU.Application.Shared.DataRequests.SchoolItems.Queries;
using CU.Application.Shared.Models.SchoolDtos;
using CU.Application.Shared.ViewModels.Instructors;
using FluentAssertions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace CU.ApplicationIntegrationTests.ApplicationTests
{
    [Collection(TestFixture.DbCollectionName)]
    public class GetInstructorsTests : ApplicationTestBase
    {
        public GetInstructorsTests(ITestOutputHelper testOutputHelper, TestFixture fixture)
            : base(testOutputHelper, fixture)
        {
        }

        [Fact]
        public async Task CanGetInstructorsList()
        {
            GetInstructorListItemsQuery query = new GetInstructorListItemsQuery();
            List<InstructorListItem> instructors = await SendAsync(query);
            instructors.Should().NotBeNullOrEmpty();
            InstructorListItem firstInstructor = instructors[0];
            instructors.Count.Should().BeGreaterThan(1);
            InstructorListItem secondInstructor = instructors[1];
            firstInstructor.Should().NotBeNull();
            secondInstructor.Should().NotBeNull();
            _testOutputHelper.WriteLine($"First Instructor ID={firstInstructor.ID}, Name=[{firstInstructor.FullName}], OfficeAssignment=[{firstInstructor.OfficeAssignment}]");
            _testOutputHelper.WriteLine($"Second Instructor ID={secondInstructor.ID}, Name=[{secondInstructor.FullName}], OfficeAssignment=[{secondInstructor.OfficeAssignment}]");
            _testOutputHelper.WriteLine($"Items.Count = {instructors.Count}");
        }

        [Fact]
        public async Task CanGetInstructorsPaginated()
        {
            GetInstructorListItemsWithPaginationQuery query = new GetInstructorListItemsWithPaginationQuery
            {
                PageNumber = 1,
                PageSize = 3
            };
            var result = await SendAsync(query);
            result.Should().NotBeNull();
            result.Items.Should().NotBeNull();
            result.Items.Count.Should().BePositive();
            result.TotalCount.Should().BeGreaterThanOrEqualTo(result.Items.Count);
            InstructorListItem firstListItem = result.Items[0];
            firstListItem.Should().NotBeNull();

            _testOutputHelper.WriteLine($"First Instructor ID = {firstListItem.ID}, Name = [{firstListItem.FullName}]");
            _testOutputHelper.WriteLine($"Items.Count = {result.Items.Count}, TotalCount = {result.TotalCount}");
        }
    }
}
