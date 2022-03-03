using AutoMapper;
using ContosoUniversity.Models;
using CU.Application.Shared.Models.SchoolDtos;

namespace CU.Application.Common.Mapping
{
    public class SchoolMappingProfile : Profile
    {
        public SchoolMappingProfile()
        {
            CreateMap<Department, DepartmentListItemDto>()
                .ForMember(
                    d => d.Administrator,
                    opt => opt.MapFrom(x =>
                        x.Administrator != null ? x.Administrator.LastName + ", " + x.Administrator.FirstMidName : null))
            ;
        }
    }
}
