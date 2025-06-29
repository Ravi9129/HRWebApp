using AutoMapper;
using HRWebApp.DTOs;
using HRWebApp.Models;
using Microsoft.AspNetCore.Identity;

namespace HRWebApp.Utilities
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Employee mappings
            CreateMap<CreateEmployeeDTO, Employee>();
            CreateMap<UpdateEmployeeDTO, Employee>();
            CreateMap<Employee, EmployeeDetailDTO>()
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department.Name))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name));

            // Department mappings
            CreateMap<CreateDepartmentDTO, Department>();
            CreateMap<UpdateDepartmentDTO, Department>();
            CreateMap<Department, DepartmentDetailDTO>()
                .ForMember(dest => dest.ManagerName, opt => opt.MapFrom(src => src.Manager != null ? $"{src.Manager.FirstName} {src.Manager.LastName}" : "N/A"))
                .ForMember(dest => dest.EmployeeCount, opt => opt.MapFrom(src => src.Employees.Count));

            // Product mappings
            CreateMap<CreateProductDTO, Product>();
            CreateMap<UpdateProductDTO, Product>();
            CreateMap<Product, ProductDetailDTO>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.AddedBy, opt => opt.MapFrom(src => $"{src.AddedBy.FirstName} {src.AddedBy.LastName}"));

            // Role mappings
            CreateMap<IdentityRole, RoleDetailDTO>();

            // User/Profile mappings
            CreateMap<ApplicationUser, ProfileDTO>();

            // Employee shift mappings
            CreateMap<EmployeeShift, EmployeeShiftDTO>();
            CreateMap<EmployeeShiftDTO, EmployeeShift>();

            // Employee bank detail mappings
            CreateMap<EmployeeBankDetail, EmployeeBankDetailDTO>();
            CreateMap<EmployeeBankDetailDTO, EmployeeBankDetail>();

            // Employee benefit mappings
            CreateMap<EmployeeBenefit, EmployeeBenefitDTO>();
            CreateMap<EmployeeBenefitDTO, EmployeeBenefit>();

            // Page access mappings
            CreateMap<CreatePageAccessDTO, PageAccess>();
            CreateMap<UpdatePageAccessDTO, PageAccess>();
            CreateMap<PageAccess, PageAccessDetailDTO>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name));
        }
    }
}
