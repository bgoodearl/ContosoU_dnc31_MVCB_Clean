using Ardalis.GuardClauses;
using CU.Application.Common.Interfaces;
using CU.Application.Shared.Interfaces;

namespace CU.Infrastructure.Repositories
{
    public class SchoolViewDataRepositoryFactory : ISchoolViewDataRepositoryFactory
    {
        public SchoolViewDataRepositoryFactory(ISchoolRepositoryFactory schoolRepositoryFactory)
        {
            Guard.Against.Null(schoolRepositoryFactory, nameof(schoolRepositoryFactory));
            SchoolRepositoryFactory = schoolRepositoryFactory;
        }

        protected ISchoolRepositoryFactory SchoolRepositoryFactory { get; }

        public ISchoolViewDataRepository GetViewDataRepository()
        {
            return new SchoolViewDataRepository(SchoolRepositoryFactory);
        }
    }
}
