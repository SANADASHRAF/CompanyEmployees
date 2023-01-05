using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Shared.DataTransferObjects;

namespace CompanyEmployees.Presentation.Controllers
{
    [Route("api/companies/[action]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public CompaniesController(IRepositoryManager _repository ,IMapper _mapper)
        {
            this._repository = _repository;
            this._mapper = _mapper;
        }



        [HttpGet (Name = "GetCompanies")]
        public IActionResult GetCompanies()
        {
            var companeies = _repository.Company.GetAllCompanies();
            var companeiesDTO=_mapper.Map<IEnumerable< CompanyDto >>(companeies);
            //with DTO only without automapper
            //.Select(c => new CompanyDto(c.Id, c.Name ?? "", string.Join(' ', c.Address, c.Country)))
            //.ToList(); ;
            return Ok(companeiesDTO); 
        }

        [HttpGet("{id:Guid}",Name = "GetCompany")]
        public IActionResult GetCompany(Guid id)
        {
            var company=_repository.Company.GetCompanyById(id);
            var companyDTO=_mapper.Map< CompanyDto>(company);
            ArgumentNullException.ThrowIfNull(companyDTO);
             return Ok(companyDTO);
        }


        [HttpPost (Name = "CreateCompany")]
        public IActionResult CreateCompany([FromBody] CompanyCreationDto company)
        {
            ArgumentNullException.ThrowIfNull(company);
            var entitcompanyEntity=_mapper.Map<Company>(company);
            _repository.Company.CreateCompany(entitcompanyEntity);
            _repository.Save();
            var companyToReturn = _mapper.Map<CompanyDto>(entitcompanyEntity);
            return CreatedAtRoute("GetCompany", new {id=companyToReturn.Id},companyToReturn);
        }



        [HttpPost(Name = "CreateCompanyWithChild")]
        public IActionResult CreateCompanyWithChild([FromBody] CompanyForCreationDto company)
        {
            ArgumentNullException.ThrowIfNull(company);
            var entitcompanyEntity = _mapper.Map<Company>(company);         
            _repository.Company.CreateCompany(entitcompanyEntity);
            _repository.Save();
            var companyToReturn = _mapper.Map<CompanyDto>(entitcompanyEntity);
            return CreatedAtRoute("GetCompany", new { id = companyToReturn.Id }, companyToReturn);
        }




    }
}
