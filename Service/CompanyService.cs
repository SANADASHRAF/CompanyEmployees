using Contracts;
using Service.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CompanyService:ICompanyService
    {
        //access the repository methods from each user repository class
        private readonly IRepositoryManager _repository;
        
        public CompanyService(IRepositoryManager repository)
          {
              _repository = repository;
          }
    }
}
