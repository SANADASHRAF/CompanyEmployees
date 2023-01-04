using AutoMapper;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Shared.DataTransferObjects;
using Entities.Models;

namespace CompanyEmployees.Presentation.Controllers
{

    [Route("api/employees/[action]")]
    [ApiController]
    public class EmployeesController :ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public EmployeesController(IRepositoryManager _repository,IMapper _mapper)
        {
            this._repository = _repository;
            this._mapper = _mapper;
        }


        [HttpGet (Name = "GetAllEmployee")]
        public IActionResult GetAllEmployee()
        {
            var Employees = _repository.Employee.GetAllEmployee();
            if (Employees == null)
                return BadRequest($"there is no employee");
            var EmployeesDTO = _mapper.Map<IEnumerable<EmployeeDto>>(Employees);
            return Ok(EmployeesDTO);
        }




        [HttpGet("{companyId:Guid}",Name = "GetEmployeesByIdForCompany")]
        public IActionResult GetEmployeesByIdForCompany(Guid companyId)
        {
            var company = _repository.Company.GetCompanyById(companyId);
            if (company is null)
                return BadRequest($"no result for company with id {companyId}");
            var employeesFromDb = _repository.Employee.GetEmployees(companyId);
            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesFromDb);
            return Ok(employeesDto);
        }




        [HttpGet("{id:Guid}",Name = "GetEmployee")]
        public IActionResult GetEmployee(Guid id)
        {
            var Employeees = _repository.Employee.GetEmployeesById(id);
            if (Employeees == null)
                return BadRequest($"no result for employee with id {id}");
            var EmployeeesDTO = _mapper.Map<EmployeeDto>(Employeees);
            return Ok(EmployeeesDTO);
        }



        [HttpPost ("{CompanyId:Guid}", Name ="CreatEmployee")]
        public IActionResult CreatEmployee(Guid CompanyId, [FromBody] EmployeeCreationDto employeeCreationDto)
        {
            var company=_repository.Company.GetCompanyById(CompanyId);
            if(company is null)
                return NotFound($"there is no company with id {CompanyId}");
            var employeeEntity=_mapper.Map<Employee>(employeeCreationDto);
            _repository.Employee.Create(CompanyId, employeeEntity);
            _repository.Save();
            var createdemployee = _mapper.Map<EmployeeDto>(employeeEntity);
            return CreatedAtRoute("GetEmployee", new { id = createdemployee.Id }, createdemployee);
        }

    }
}
