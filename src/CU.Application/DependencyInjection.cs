using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using CU.Application.Common.Mapping;
using CU.Application.Common.Behaviors;
using FluentValidation;

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
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        }
    }
}
