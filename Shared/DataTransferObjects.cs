using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class DataTransferObjects
    {
        public record CompanyDto(Guid Id, string Name, string FullAddress);
        public record CompanyCreationDto(string Name, string Address, string Country);
        public record CompanyForCreationDto(string Name, string Address, string Country,
                                     IEnumerable<EmployeeCreationDto> Employees);
        public record CompanyForUpdateDto(string Name, string Address, string Country,
                                    IEnumerable<EmployeeCreationDto> Employees);

        

        public record EmployeeDto(Guid Id, string Name, int Age, string Position);
        public record EmployeeCreationDto(string Name, int Age, string Position);
        public record EmployeeForUpdateDto(string Name, int Age, string Position);
       


    }
}
