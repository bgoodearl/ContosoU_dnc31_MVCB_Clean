﻿using CU.Application.Common.Interfaces;
using CU.Infrastructure;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace CU.ApplicationIntegrationTests
{
    public class TestFixture : TestBedFixture
    {
        public const string DbCollectionName = "DatabaseCollection";
        private static int fixtureInstanceCount = 0;
        internal static bool FixtureDbInitialized { get; private set; }

        public TestFixture()
        {
            ++fixtureInstanceCount;
        }

        internal async Task<ISchoolDbContext> GetISchoolDbContext(ITestOutputHelper testOutputHelper)
        {
            testOutputHelper.Should().NotBeNull();

            ISchoolDbContextFactory schoolDbContextFactory = GetService<ISchoolDbContextFactory>(testOutputHelper);
            schoolDbContextFactory.Should().NotBeNull();
            if (schoolDbContextFactory != null)
            {
                ISchoolDbContext cuContext = schoolDbContextFactory.GetSchoolDbContext();
                cuContext.Should().NotBeNull();
                if (cuContext != null)
                {
                    if (!FixtureDbInitialized)
                    {
                        int saveCount = await cuContext.SeedInitialDataAsync();
                        FixtureDbInitialized = true;
                        if (testOutputHelper != null)
                        {
                            testOutputHelper.WriteLine($"SeedInitialDataAsync saved {saveCount} changes, fixtureInstanceCount = {fixtureInstanceCount}");
                        }
                    }
                    return cuContext;
                }
            }

            throw new InvalidOperationException("GetISchoolDbContext - invalid configuration");
        }

        #region TestBed

        protected override string GetConfigurationFile()
        {
            return "appsettings.LocalTesting.json";
        }

        protected override void AddServices(IServiceCollection services, IConfiguration configuration)
        {
            if (configuration != null)
            {
                services.AddInfrastructure(configuration);
            }
        }

        protected override ValueTask DisposeAsyncCore()
        {
            return new ValueTask();
        }

        #endregion TestBed

    }
}
