using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using CU.Application.Common.Mapping;

namespace CU.Application
{
    public static class DependencyInjection
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.ConstructServicesUsing(t => services.BuildServiceProvider().GetRequiredService(t));
                config.AddProfile<SchoolMappingProfile>();
            });
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
