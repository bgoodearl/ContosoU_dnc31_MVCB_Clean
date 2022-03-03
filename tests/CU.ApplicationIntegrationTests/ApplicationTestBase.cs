using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace CU.ApplicationIntegrationTests
{
    public class ApplicationTestBase : DbContextTestBase
    {
        public ApplicationTestBase(ITestOutputHelper testOutputHelper, TestFixture fixture)
            : base (testOutputHelper, fixture)
        {
        }

        protected async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            _testOutputHelper.Should().NotBeNull();
            if (_testOutputHelper != null)
            {
                IServiceScopeFactory scopeFactory = _fixture.GetService<IServiceScopeFactory>(_testOutputHelper);
                scopeFactory.Should().NotBeNull();

                if (scopeFactory != null)
                {
                    using var scope = scopeFactory.CreateScope();

                    var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

                    return await mediator.Send(request);
                }
            }
            throw new InvalidOperationException("Unexpected problem setting up for SendAsync");
        }

    }
}
