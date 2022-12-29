using Contracts;
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
        private readonly IRepositoryManager x;
        public CompaniesController(IRepositoryManager x)
        {
            this.x = x;
        }

        [HttpGet]
        public IActionResult GetCompanies()
        {
            var companeies = x.Company.GetAllCompanies();
            var z=companeies.Select( c =>new CompanyDto(c.Id, c.Name ?? "", string.Join(' ',c.Address, c.Country)))
            .ToList();
            return Ok(z);
        }
        }
}
