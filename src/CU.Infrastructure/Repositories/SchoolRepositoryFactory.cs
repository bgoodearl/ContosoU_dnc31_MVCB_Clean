using CU.Application.Common.Interfaces;
using CU.Infrastructure.Persistence;
using System;

namespace CU.Infrastructure.Repositories
{
    public class SchoolRepositoryFactory : ISchoolRepositoryFactory
    {
        private string NameOrConnectionString { get; }
        public SchoolRepositoryFactory(string nameOrConnectionString)
        {
            NameOrConnectionString = nameOrConnectionString ?? throw new ArgumentNullException(nameof(nameOrConnectionString));
        }

        public ISchoolRepository GetSchoolRepository()
        {
            return new SchoolRepository(new SchoolDbContext(NameOrConnectionString));
        }

    }
}
