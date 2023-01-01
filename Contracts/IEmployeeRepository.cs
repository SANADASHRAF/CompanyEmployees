using Entities.Models;
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
         Employee? GetEmployeesById(Guid id);

        IEnumerable<Employee> GetEmployees(Guid companyId);
    }
}
