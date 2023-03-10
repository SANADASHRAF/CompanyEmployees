using Entities.Models;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAllEmployee();
        IEnumerable<Employee> FilterEmployeeWithAge(EmployeeParameters employeeParameters);
        Employee? GetEmployeesById(Guid id);
        IEnumerable<Employee> GetEmployees(Guid companyId);
        void Create(Guid CompanyId, Employee employee);
        void DeleteEmployee(Employee employee);
        void UpdateEmployee(Employee employee);

    }
}
