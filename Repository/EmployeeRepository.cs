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

        public Employee? GetEmployeesById(Guid id)
        {
            return FindByCondition(x => x.Id.Equals(id)).OrderBy(x => x.Name).SingleOrDefault();
        }

        public IEnumerable<Employee> GetEmployees(Guid companyId)
        {
           return  FindByCondition(e => e.CompanyId.Equals(companyId)).OrderBy(e => e.Name).ToList();

        }
        public void Create(Guid CompanyId, Employee employee)
        {
            employee.CompanyId = CompanyId;
            Create(employee);
        }

        public void DeleteEmployee(Employee employee)
        {
           Delete(employee);
        }

        public void UpdateEmployee(Employee employee)
        {
            Update(employee);
        }
    }
}
