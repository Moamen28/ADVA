using AutoMapper;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AutoMapper
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            // CreateMap<source, destination>()
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeCreateDTO, Employee>();
            // Add other mappings here
            CreateMap<EmployeeDto, Employee>();
            CreateMap<EmployeeDto, Employee>();
            // to get all employee and their manger name
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.ManagerName, opt => opt.MapFrom(src => src.Manager.Name));

        }
    }
}
