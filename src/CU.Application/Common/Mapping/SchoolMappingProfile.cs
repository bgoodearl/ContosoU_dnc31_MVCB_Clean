using AutoMapper;
using ContosoUniversity.Models;
using CU.Application.Shared.Models.SchoolDtos;
using CU.Application.Shared.ViewModels.Instructors;

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
            CreateMap<Instructor, InstructorListItem>()
                .ForMember(d => d.ID, opt => opt.MapFrom(x => x.ID))
                .ForMember(
                    d => d.OfficeAssignment,
                    opt => opt.MapFrom(x =>
                        x.OfficeAssignment != null ? x.OfficeAssignment.Location : null))
            ;
        }
    }
}
