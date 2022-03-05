using CU.Application.Shared.Common.Exceptions;
using CU.Application.Data.Common.Interfaces;
using CU.Application.Shared.DataRequests.SchoolItems.Commands;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using CM = ContosoUniversity.Models;

namespace CU.ApplicationIntegrationTests.ApplicationTests.Students
{
    [Collection(TestFixture.DbCollectionName)]
    public class UpdateStudentItemTests : ApplicationTestBase
    {
        public UpdateStudentItemTests(ITestOutputHelper testOutputHelper, TestFixture fixture)
            : base(testOutputHelper, fixture)
        {
        }
    }
}
