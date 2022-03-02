using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Abstractions;

namespace CU.ApplicationIntegrationTests
{
    public class DbContextTestBase
    {
        internal ITestOutputHelper _testOutputHelper { get; }
        internal TestFixture _fixture { get; }

        public DbContextTestBase(ITestOutputHelper testOutputHelper, TestFixture fixture)
        {
            fixture.Should().NotBeNull();
            testOutputHelper.Should().NotBeNull();
            if ((fixture != null) && (testOutputHelper != null))
            {
                _fixture = fixture;
                _testOutputHelper = testOutputHelper;
            }
            else
            {
                throw new InvalidOperationException("ApplicationTestBase - _fixture or _testOutputHelper is null");
            }
        }

    }
}
