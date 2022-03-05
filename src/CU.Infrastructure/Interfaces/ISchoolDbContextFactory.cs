using CU.Application.Data.Common.Interfaces;

namespace CU.Infrastructure.Interfaces
{
    public interface ISchoolDbContextFactory
    {
        ISchoolDbContext GetSchoolDbContext();
    }
}
