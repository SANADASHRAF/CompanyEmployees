using AutoMapper;
using Entities.Models;
using static Shared.DataTransferObjects;

namespace CompanyEmployees
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            // destination to  -> sourse(in controller)
            // sourse to       -> destination (while creating)
            //_mapper.Map<Employee>(employeeCreationDto);
            //_mapper.Map(employeeForUpdate, employeeEntity) "لو هتباصى لاول ارجيزمنت اوبجكت"



            CreateMap<Company, CompanyDto>()
            .ForCtorParam("FullAddress",
            opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));

            CreateMap<Employee, EmployeeDto>();

            CreateMap<CompanyCreationDto, Company>();
            CreateMap<CompanyForCreationDto, Company>();
            CreateMap<EmployeeCreationDto, Employee>();
        }

    }
}
