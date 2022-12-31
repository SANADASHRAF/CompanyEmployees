using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class EmployeeRepository:RepositoryBase<Employee>,IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return FiindAll().OrderBy(x => x.Name).ToList();
        }

        public IEnumerable<Employee> GetEmployeesByIdForCompany(Guid id)
        {
            return FindByCondition(x => x.CompanyId.Equals(id)).OrderBy(x => x.Name).ToList();
        }
    }
}
