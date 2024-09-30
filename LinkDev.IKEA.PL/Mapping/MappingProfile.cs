using AutoMapper;
using LinkDev.IKEA.BLL.Models.Departments;
using LinkDev.IKEA.PL.ViewModels.Departments;

namespace LinkDev.IKEA.PL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            #region EmployeeProfile

            #endregion

            #region DepartmentProfile
            CreateMap<DepartmentDetailsDto, DepartmentViewModel>()
                .ForMember(destination => destination.Name,config => config.MapFrom(src => src.Name));

            CreateMap<DepartmentViewModel, UpdatedDepartmentDto>();

            CreateMap<DepartmentViewModel, CreatedDepartmentDto>();
                
            #endregion
        }
    }
}
