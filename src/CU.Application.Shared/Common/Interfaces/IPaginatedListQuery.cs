
namespace CU.Application.Shared.Common.Interfaces
{
    public interface IPaginatedListQuery
    {
        int PageNumber { get; set; }
        int PageSize { get; set; }
    }
}
