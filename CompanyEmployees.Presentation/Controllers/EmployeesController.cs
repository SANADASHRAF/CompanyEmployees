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

    [Route("api/employees")]
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

        [HttpGet]
        public IActionResult GetAllEmployee()
        {
            var Employees=_repository.Employee.GetAllEmployee();
            var EmployeesDTO = _mapper.Map< IEnumerable<EmployeeDto>>(Employees);
            return Ok(EmployeesDTO);
        }

        [HttpGet]
        public IActionResult GetEmployeesForCompany(Guid id)
        {
            var Employees=_repository.Employee.GetEmployeesByIdForCompany(id);
            var EmployeesDTO = _mapper.Map<IEnumerable<EmployeeDto>>(Employees);
            return Ok(EmployeesDTO);
        }
    }
}
