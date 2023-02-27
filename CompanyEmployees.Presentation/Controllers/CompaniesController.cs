using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
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
        public CompaniesController(IRepositoryManager _repository, IMapper _mapper)
        {
            this._repository = _repository;
            this._mapper = _mapper;
        }


        [Authorize (Roles = "Administrator")]
        [HttpGet(Name = "GetCompanies")]
        
        public IActionResult GetCompanies()
        {
            var companeies = _repository.Company.GetAllCompanies();
            var companeiesDTO = _mapper.Map<IEnumerable<CompanyDto>>(companeies);
            //with DTO only without automapper
            //.Select(c => new CompanyDto(c.Id, c.Name ?? "", string.Join(' ', c.Address, c.Country)))
            //.ToList(); ;
            return Ok(companeiesDTO);
        }

        [HttpGet("{id:Guid}", Name = "GetCompany")]
        public IActionResult GetCompany(Guid id)
        {
            var company = _repository.Company.GetCompanyById(id);
            var companyDTO = _mapper.Map<CompanyDto>(company);
            ArgumentNullException.ThrowIfNull(companyDTO);
            return Ok(companyDTO);
        }


        [HttpPost(Name = "CreateCompany")]
        public IActionResult CreateCompany([FromBody] CompanyCreationDto company)
        {
            ArgumentNullException.ThrowIfNull(company);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var entitcompanyEntity = _mapper.Map<Company>(company);
            _repository.Company.CreateCompany(entitcompanyEntity);
            _repository.Save();
            var companyToReturn = _mapper.Map<CompanyDto>(entitcompanyEntity);
            return CreatedAtRoute("GetCompany", new { id = companyToReturn.Id }, companyToReturn);
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


        [HttpDelete("{companyid:Guid}",Name = "DeleteCompany")]
        public IActionResult DeleteCompany(Guid companyid)
        {
            var company=_repository.Company.GetCompanyById(companyid);
            ArgumentNullException.ThrowIfNull(company);
            _repository.Company.DeleteCompany(company);
            _repository.Save();
            return Ok($"Company with id {companyid} already has been deleted");
        }


        [HttpPut ("{CompanyId:Guid}", Name = "UpdateCompanyWithInsertion")]
        public IActionResult UpdateCompanyWithInsertion([FromBody] CompanyForUpdateWithInsertChieldDto company,Guid CompanyId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest($"the company with id {CompanyId} not found");
            }
            else
            {
                var SelectedCompany = _repository.Company.GetCompanyById(CompanyId);
                ArgumentNullException.ThrowIfNull(SelectedCompany);
                var CompanyEntity = _mapper.Map(company, SelectedCompany);
                _repository.Company.UpdateCompany(CompanyEntity);
                _repository.Save();
                return Ok($"the company with id {CompanyId} has been updeted successfully");
            }
        }

        [HttpPut("{CompanyId:Guid}", Name = "UpdateCompanyWithOutInsertion")]
        public IActionResult UpdateCompanyWithOutInsertion([FromBody] CompanyForUpdateWithOutInsertChieldDto company, Guid CompanyId)
        {
            var SelectedCompany = _repository.Company.GetCompanyById(CompanyId);
            ArgumentNullException.ThrowIfNull(SelectedCompany);
            var CompanyEntity = _mapper.Map(company, SelectedCompany);
            _repository.Company.UpdateCompany(CompanyEntity);
            _repository.Save();
            return Ok($"the company with id {CompanyId} has been updeted successfully");
        }

    }
}
