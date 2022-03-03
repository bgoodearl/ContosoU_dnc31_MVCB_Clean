using CU.Application.Shared.DataRequests.SchoolItems.Queries;
using CU.Application.Shared.ViewModels.Students;
using FluentAssertions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace CU.ApplicationIntegrationTests.ApplicationTests
{
    [Collection(TestFixture.DbCollectionName)]
    public class GetStudentsTests : ApplicationTestBase
    {
        public GetStudentsTests(ITestOutputHelper testOutputHelper, TestFixture fixture)
            : base(testOutputHelper, fixture)
        {
        }


        [Fact]
        public async Task CanGetStudentsList()
        {
            GetStudentListItemsQuery query = new GetStudentListItemsQuery();
            List<StudentListItem> students = await SendAsync(query);
            students.Should().NotBeNullOrEmpty();
            StudentListItem firstStudent = students[0];
            students.Count.Should().BeGreaterThan(1);
            StudentListItem secondStudent = students[1];
            firstStudent.Should().NotBeNull();
            secondStudent.Should().NotBeNull();
            _testOutputHelper.WriteLine($"First Student ID={firstStudent.ID}, Name=[{firstStudent.FullName}]");
            _testOutputHelper.WriteLine($"Second Student ID={secondStudent.ID}, Name=[{secondStudent.FullName}]");
            _testOutputHelper.WriteLine($"Items.Count = {students.Count}");
        }


        [Fact]
        public async Task CanGetStudentsPaginated()
        {
            GetStudentListItemsWithPaginationQuery query = new GetStudentListItemsWithPaginationQuery
            {
                PageNumber = 1,
                PageSize = 5
            };

            var result = await SendAsync(query);
            result.Should().NotBeNull();
            result.Items.Should().NotBeNull();
            result.Items.Count.Should().BePositive();
            result.TotalCount.Should().BeGreaterThanOrEqualTo(result.Items.Count);
            StudentListItem firstListItem = result.Items[0];
            firstListItem.Should().NotBeNull();

            _testOutputHelper.WriteLine($"First Student ID = {firstListItem.ID}, Name = [{firstListItem.FullName}]");
            _testOutputHelper.WriteLine($"Items.Count = {result.Items.Count}, TotalCount = {result.TotalCount}");
        }

    }
}
