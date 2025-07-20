using AutoMapper;
using Emi.Employees.Application.DTO;
using Emi.Employees.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emi.Employees.Application.Mappings
{
    public class EmployeeMapperProfile : Profile
    {
        public EmployeeMapperProfile()
        {
            CreateMap<EmployeeDTO, EmployeeEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CurrentPosition, opt => opt.MapFrom(src => src.CurrentPosition))
                .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Salary));
            //.ForAllMembers(opts => opts.PreCondition((src, dest, srcMember))
            //    => srcMember != null && !string.IsNullOrWhiteSpace(srcMember.ToString())

            CreateMap<UserRequest, UserEntity>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.UserRole));
        }
    }
}
