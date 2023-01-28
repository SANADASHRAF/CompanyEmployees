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

        public record CompanyCreationDto {
            [Required(ErrorMessage = "Company name is a required field.")]
            [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
            [MinLength(3, ErrorMessage = "minimum length for the Name is 3 characters.")]
            public string? Name { get; init; }

            [Required(ErrorMessage = "Company Adress name is a required field.")]
            [MaxLength(30, ErrorMessage = "Maximum length for the Adress is 30 characters.")]
            [MinLength(3, ErrorMessage = "minimum length for the Adress is 3 characters.")]
            public string? Address { get; init; }

            [Required(ErrorMessage = "Company name is a required field.")]
            [MaxLength(30, ErrorMessage = "Maximum length for the Country is 30 characters.")]
           
            public string? Country { get; init; }
        };

        public record CompanyForCreationDto(string Name, string Address, string Country,
                                     IEnumerable<EmployeeCreationDto> Employees);
        public record CompanyForUpdateWithInsertChieldDto(string Name, string Address, string Country,
                                    IEnumerable<EmployeeCreationDto> Employees);
       
        public record CompanyForUpdateWithOutInsertChieldDto(string Name, string Address, string Country);

        public record EmployeeDto(Guid Id, string Name, int Age, string Position);

        public record EmployeeForManipulationDto
        {
            [Required(ErrorMessage = "Employee name is a required field.")]
            [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
            [MinLength(3, ErrorMessage = "minimum length for the Name is 3 characters.")]
            public string? Name { get; init; }

            [Range(18, int.MaxValue, ErrorMessage = "Age is required and it can't be lower than  18")]
            public int? Age { get; init; }

            [Required(ErrorMessage = "Position is a required field.")]
            [MaxLength(20, ErrorMessage = "Maximum length for the Position is 20 characters.")]
            public string? Position { get; init; }
        };
        public record EmployeeCreationDto : EmployeeForManipulationDto;



        public record EmployeeForUpdateDto: EmployeeForManipulationDto;





    }
}
