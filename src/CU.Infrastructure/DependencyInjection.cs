using Ardalis.GuardClauses;
using CU.Application.Common.Interfaces;
using CU.Application.Data.Common.Interfaces;
using CU.Application.Shared.Interfaces;
using CU.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CIP = CU.Infrastructure.Persistence;
using CIR = CU.Infrastructure.Repositories;

namespace CU.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            string connStr = configuration["ConnectionStrings:SchoolDbContext"];
            Guard.Against.NullOrWhiteSpace(connStr, "configuration[ConnectionStrings:SchoolDbContext]");

            services.AddSingleton<ISchoolDbContextFactory>(sp => new CIP.SchoolDbContextFactory(connStr));
            services.AddScoped<ISchoolDbContext>(sp => sp.GetRequiredService<ISchoolDbContextFactory>().GetSchoolDbContext());

            //Inject Entity Framework Repository and DbContext Factories
            services.AddSingleton<ISchoolRepositoryFactory>(sp => new CIR.SchoolRepositoryFactory(connStr));
            services.AddSingleton<ISchoolViewDataRepositoryFactory, CIR.SchoolViewDataRepositoryFactory>();

            return services;
        }
    }
}
