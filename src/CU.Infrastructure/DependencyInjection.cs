using Ardalis.GuardClauses;
using CU.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CIP = CU.Infrastructure.Persistence;

namespace CU.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<ISchoolDbContext>(sp => sp.GetRequiredService<ISchoolDbContextFactory>().GetSchoolDbContext());

            string connStr = configuration["ConnectionStrings:SchoolDbContext"];
            Guard.Against.NullOrWhiteSpace(connStr, "configuration[ConnectionStrings:SchoolDbContext]");

            //Inject Entity Framework Repository and DbContext Factories
            services.AddSingleton<ISchoolDbContextFactory>(sp => new CIP.SchoolDbContextFactory(connStr));

            return services;
        }
    }
}
