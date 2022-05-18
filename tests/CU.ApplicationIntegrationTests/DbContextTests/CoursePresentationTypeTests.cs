using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Models.Lookups;
using CU.Application.Data.Common.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;
using CM = ContosoUniversity.Models;
using CML = ContosoUniversity.Models.Lookups;

namespace CU.ApplicationIntegrationTests.DbContextTests
{
    [Collection(TestFixture.DbCollectionName)]
    public class CoursePresentationTypeTests : DbContextTestBase
    {
        public CoursePresentationTypeTests(ITestOutputHelper testOutputHelper, TestFixture fixture)
            : base(testOutputHelper, fixture)
        {
        }

        [SkippableFact]
        public async Task CanGetCoursePresentationTypes()
        {
            using (var scope = _fixture.GetServiceScopeFactory(_testOutputHelper).CreateScope())
            {
                ISchoolDbContext cuContext = scope.ServiceProvider.GetRequiredService<ISchoolDbContext>();
                List<CoursePresentationType> lookupList = await cuContext.CoursePresentationTypes.ToListAsync();
                lookupList.Should().NotBeNull();
                Skip.If(lookupList.Count == 0, "No lookups in list - cannot complete test");
            }
        }
    }
}
