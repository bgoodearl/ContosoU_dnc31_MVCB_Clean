using CU.Application.Shared.DataRequests.SchoolItems.Queries;
using CU.Application.Shared.Models.SchoolDtos;
using FluentAssertions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace CU.ApplicationIntegrationTests.ApplicationTests
{
    [Collection(TestFixture.DbCollectionName)]
    public class GetDepartmentsTests : ApplicationTestBase
    {
        public GetDepartmentsTests(ITestOutputHelper testOutputHelper, TestFixture fixture)
            : base(testOutputHelper, fixture)
        {
        }

        [Fact]
        public async Task CanGetDepartmentsList()
        {
            GetDepartmentListItemsQuery query = new GetDepartmentListItemsQuery();
            List<DepartmentListItemDto> departments = await SendAsync(query);
            departments.Should().NotBeNullOrEmpty();
            DepartmentListItemDto firstDepartment = departments[0];
            firstDepartment.Should().NotBeNull();
            _testOutputHelper.WriteLine($"First Dept DepartmentID={firstDepartment.DepartmentID}, Name=[{firstDepartment.Name}], Administrator=[{firstDepartment.Administrator}]");
            _testOutputHelper.WriteLine($"Items.Count = {departments.Count}");
        }
    }
}
