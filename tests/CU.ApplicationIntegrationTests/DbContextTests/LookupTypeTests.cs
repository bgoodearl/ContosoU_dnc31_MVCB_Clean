﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Models.Lookups;
using CU.Application.Data.Common.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;

namespace CU.ApplicationIntegrationTests.DbContextTests
{
    [Collection(TestFixture.DbCollectionName)]
    public class LookupTypeTests : DbContextTestBase
    {
        public LookupTypeTests(ITestOutputHelper testOutputHelper, TestFixture fixture)
            : base(testOutputHelper, fixture)
        {
        }

        [SkippableFact]
        public async Task CanGetCoursePresentationTypes()
        {
            using (var scope = _fixture.GetServiceScopeFactory(_testOutputHelper).CreateScope())
            {
                const string lookupTypeName = nameof(CoursePresentationType);

                ISchoolDbContext cuContext = scope.ServiceProvider.GetRequiredService<ISchoolDbContext>();
                List<CoursePresentationType> lookupList = await cuContext.CoursePresentationTypes.ToListAsync();
                lookupList.Should().NotBeNull();
                Skip.If(lookupList.Count == 0, "No lookups in list - cannot complete test");
                _testOutputHelper.WriteLine($"Found {lookupList.Count} lookups of type {lookupTypeName}");
                var firstLookup = lookupList.OrderBy(l => l.Code).First();
                firstLookup.Should().NotBeNull();
                _testOutputHelper.WriteLine($"First lookup of type {lookupTypeName} Code = [{firstLookup.Code}], Name = [{firstLookup.Name}]");
                var lastLookup = lookupList.OrderBy(l => l.Code).Last();
                lastLookup.Should().NotBeNull();
                _testOutputHelper.WriteLine($"Last lookup of type {lookupTypeName}  Code = [{lastLookup.Code}], Name = [{lastLookup.Name}]");
            }
        }

        [SkippableFact]
        public async Task CanDepartmentFacilityTypes()
        {
            using (var scope = _fixture.GetServiceScopeFactory(_testOutputHelper).CreateScope())
            {
                const string lookupTypeName = nameof(DepartmentFacilityType);

                ISchoolDbContext cuContext = scope.ServiceProvider.GetRequiredService<ISchoolDbContext>();
                List<DepartmentFacilityType> lookupList = await cuContext.DepartmentFacilityTypes.ToListAsync();
                lookupList.Should().NotBeNull();
                Skip.If(lookupList.Count == 0, "No lookups in list - cannot complete test");
                _testOutputHelper.WriteLine($"Found {lookupList.Count} lookups of type {lookupTypeName}");
                var firstLookup = lookupList.OrderBy(l => l.Code).First();
                firstLookup.Should().NotBeNull();
                _testOutputHelper.WriteLine($"First lookup of type {lookupTypeName} Code = [{firstLookup.Code}], Name = [{firstLookup.Name}]");
                var lastLookup = lookupList.OrderBy(l => l.Code).Last();
                lastLookup.Should().NotBeNull();
                _testOutputHelper.WriteLine($"Last lookup of type {lookupTypeName}  Code = [{lastLookup.Code}], Name = [{lastLookup.Name}]");
            }
        }
    }
}
