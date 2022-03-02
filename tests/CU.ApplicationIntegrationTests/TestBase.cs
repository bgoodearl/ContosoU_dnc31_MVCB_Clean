using FluentAssertions;
using Microsoft.Extensions.Configuration;

namespace CU.ApplicationIntegrationTests
{
    public class TestBase
    {
        internal static string[] ConfigurationFiles =
        {
            "appsettings.json",
            "appsettings.LocalTesting.json"
        };
        protected IConfigurationRoot GetConfiguration()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true)
                .AddJsonFile("appsettings.LocalTesting.json", true)
                .Build();
            config.Should().NotBeNull();
            return config;
        }
    }
}
