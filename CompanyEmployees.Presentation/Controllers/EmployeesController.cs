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
using Shared.RequestFeatures;

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


        
        [HttpGet(Name = "GetEmployeesWithAge")]
        public IActionResult GetEmployeesWithAge([FromBody]EmployeeParameters employeeParameters)
        {
            if (!employeeParameters.ValidAgeRange)
                return BadRequest("Max age can't be less than min age");

            var Employees = _repository.Employee.FilterEmployeeWithAge(employeeParameters);
            if (Employees.Count()==0)
                return BadRequest($"there is no employee in this range");
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

            ArgumentNullException.ThrowIfNull(employeeCreationDto);
            var company=_repository.Company.GetCompanyById(CompanyId);

            //if(company is null)
            //    return BadRequest($"there is no company with id {CompanyId}");
            ArgumentNullException.ThrowIfNull(company);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var employeeEntity=_mapper.Map<Employee>(employeeCreationDto);
            _repository.Employee.Create(CompanyId, employeeEntity);
            _repository.Save();
            var createdemployee = _mapper.Map<EmployeeDto>(employeeEntity);
            return CreatedAtRoute("GetEmployee", new { id = createdemployee.Id }, createdemployee);
        }



        [HttpDelete ("{employeeId:Guid}",Name = "DeleteEmployee")]
        public IActionResult DeleteEmployee(Guid employeeId)
        {
            var employee = _repository.Employee.GetEmployeesById(employeeId);
            ArgumentNullException.ThrowIfNull(employee);
            _repository.Employee.DeleteEmployee(employee);
            _repository.Save();
            return NoContent();
        }

        [HttpPut ("{employeeid:Guid}" ,Name = "UpdateEmployee")]
        public IActionResult UpdateEmployee([FromBody]EmployeeCreationDto employee,Guid employeeid)
        {
            var employeeEntity = _repository.Employee.GetEmployeesById(employeeid);
            ArgumentNullException.ThrowIfNull(employeeEntity) ;
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);
            _mapper.Map( employee,employeeEntity);
            _repository.Save();
            return Ok("Created Successfully");

        }

    }
}
