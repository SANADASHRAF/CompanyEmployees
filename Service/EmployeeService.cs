using Contracts;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public sealed class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryManager _repository;
       
        public EmployeeService(IRepositoryManager repository)
        {
            _repository = repository;
            
        }
    }
}
