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
    public class CreateStudentItemTests : ApplicationTestBase
    {
        public CreateStudentItemTests(ITestOutputHelper testOutputHelper, TestFixture fixture)
            : base(testOutputHelper, fixture)
        {
        }

        [Fact]
        public async Task ShouldRequireMinimumFields()
        {
            var command = new CreateStudentItemCommand();

            var result = await FluentActions.Invoking(() =>
                SendAsync(command)).Should().ThrowAsync<ValidationException>();

            _testOutputHelper.WriteLine($"result = [{result}]");
            result.And.Should().BeOfType<ValidationException>();
            ValidationException validationException = result.And;
            validationException.Errors.Should().NotBeNullOrEmpty();
            _testOutputHelper.WriteLine("Errors:");
            foreach(KeyValuePair<string, string[]> kv in validationException.Errors)
            {
                _testOutputHelper.WriteLine($"{kv.Key}: [{kv.Value[0]} (1 of {kv.Value.Length})]");
            }
            validationException.Errors.Count.Should().Be(2);
        }

        [SkippableFact]
        public async Task ShouldCreateStudent()
        {
            const string lastNameSmith = "Smith";
            const string firstNameStartsWithJohn = "John";
            CM.Student latestJohnSmith = null;
            using (ISchoolDbContext cuContext = await _fixture.GetISchoolDbContext(_testOutputHelper))
            {
                latestJohnSmith = await cuContext.Students
                    .Where(s => s.LastName == lastNameSmith && s.FirstMidName.StartsWith(firstNameStartsWithJohn))
                    .OrderByDescending(s => s.FirstMidName)
                    .FirstOrDefaultAsync();
            }

            string firstMidName = "John A";

            if (latestJohnSmith != null)
            {
                char prevInitial = latestJohnSmith.FirstMidName[latestJohnSmith.FirstMidName.Length - 1];
                if (prevInitial <= 'Z')
                {
                    char newInitial = ++prevInitial;
                    firstMidName = $"{firstNameStartsWithJohn} {newInitial}";
                }
                else
                {
                    Skip.If(true, $"FirstMid Name = [{latestJohnSmith.FirstMidName}]");
                }
            }

            var command = new CreateStudentItemCommand
            {
                LastName = lastNameSmith,
                FirstMidName = firstMidName,
                EnrollmentDate = DateTime.Now.Date
            };

            var itemId = await SendAsync(command);

            var item = await GetStudentAsync(itemId);
            item.Should().NotBeNull();
            if (item != null)
            {
                _testOutputHelper.WriteLine($"Created Student ID = {item.ID}, Last = [{item.LastName}], FirstMid = [{item.FirstMidName}]");
                item.LastName.Should().Be(lastNameSmith);
                item.FirstMidName.Should().Be(firstMidName);
            }
        }

    }
}
