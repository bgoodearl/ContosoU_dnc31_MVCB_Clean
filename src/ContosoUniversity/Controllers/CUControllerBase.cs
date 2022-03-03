using Ardalis.GuardClauses;
using CU.Application.Common.Interfaces;
using CU.Application.Shared.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace ContosoUniversity.Controllers
{
    public class CUControllerBase : Controller
    {
        public CUControllerBase(IHttpContextAccessor httpContextAccessor)
        {
            Guard.Against.Null(httpContextAccessor, nameof(httpContextAccessor));
            HttpContextAccessor = httpContextAccessor;
            Guard.Against.Null(httpContextAccessor.HttpContext, nameof(httpContextAccessor.HttpContext));
            Guard.Against.Null(httpContextAccessor.HttpContext.RequestServices, nameof(httpContextAccessor.HttpContext.RequestServices));
            SchoolDbContextFactory = httpContextAccessor.HttpContext.RequestServices.GetRequiredService<ISchoolDbContextFactory>();
            SchoolRepositoryFactory = httpContextAccessor.HttpContext.RequestServices.GetRequiredService<ISchoolRepositoryFactory>();
            SchoolViewDataRepositoryFactory = httpContextAccessor.HttpContext.RequestServices.GetRequiredService<ISchoolViewDataRepositoryFactory>();
            Mediator = httpContextAccessor.HttpContext.RequestServices.GetRequiredService<ISender>();
        }

        #region Read Only variables

        protected IHttpContextAccessor HttpContextAccessor { get; }
        protected ISender Mediator { get; }

        #endregion Read Only variables


        #region Repository/Database

        ISchoolViewDataRepositoryFactory SchoolViewDataRepositoryFactory { get; }
        protected ISchoolViewDataRepository GetSchoolViewDataRepository()
        {
            return SchoolViewDataRepositoryFactory.GetViewDataRepository();
        }

        ISchoolDbContextFactory SchoolDbContextFactory { get; }
        protected ISchoolDbContext GetSchoolDbContext()
        {
            return SchoolDbContextFactory.GetSchoolDbContext();
        }

        protected ISchoolRepositoryFactory SchoolRepositoryFactory { get; }
        protected ISchoolRepository GetSchoolRepository()
        {
            return SchoolRepositoryFactory.GetSchoolRepository();
        }

        #endregion Repository/Database

        protected async Task<TResponse> SendQueryAsync<TResponse>(IRequest<TResponse> request)
        {
            return await Mediator.Send(request);
        }

    }
}
