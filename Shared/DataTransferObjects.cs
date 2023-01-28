using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public record CompanyForUpdateWithInsertChieldDto(string Name, string Address, string Country,
                                    IEnumerable<EmployeeCreationDto> Employees);
       
        public record CompanyForUpdateWithOutInsertChieldDto(string Name, string Address, string Country);

        public record EmployeeDto(Guid Id, string Name, int Age, string Position);

        public record EmployeeCreationDto
        {
            [Required(ErrorMessage = "Employee name is a required field.")]
            [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
            [MinLength(3, ErrorMessage = "minimum length for the Name is 3 characters.")]
            public string? Name { get; init; }

            [Required(ErrorMessage = "Age is a required field.")]
            public int? Age { get; init; }

            [Required(ErrorMessage = "Position is a required field.")]
            [MaxLength(20, ErrorMessage = "Maximum length for the Position is 20 characters.")]
            public string? Position { get; init; }
        };

        public record EmployeeForUpdateDto(string Name, int Age, string Position);
       


    }
}
