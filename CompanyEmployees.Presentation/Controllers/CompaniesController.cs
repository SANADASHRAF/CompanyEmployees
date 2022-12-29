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
    [Route("api/companies")]
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



        [HttpGet]
        public IActionResult GetCompanies()
        {


            var companeies = _repository.Company.GetAllCompanies();
            var companeiesDTO=_mapper.Map<IEnumerable< CompanyDto >>(companeies);
            //.Select(c => new CompanyDto(c.Id, c.Name ?? "", string.Join(' ', c.Address, c.Country)))
            //.ToList(); ;

            return Ok(companeiesDTO); 
        }
        }
}
