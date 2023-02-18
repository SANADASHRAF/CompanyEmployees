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
        

        //this for instead of repeat this validation in every record that have same properity we use inheritanse
        public record CompanyWithoutchiledForManipulationDto
        {
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
        }

        public record CompanyWithChieldForManipulationDto
        {
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

            [Required(ErrorMessage = "you should have add empioyees")]
            public IEnumerable<EmployeeCreationDto>? Employees;
        };

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

        //identity(authontication)
        public record UserForRegistrationDto
        {
            public string? FirstName { get; init; }
            public string? LastName { get; init; }
            [Required(ErrorMessage = "Username is required")]
            public string? UserName { get; init; }
            [Required(ErrorMessage = "Password is required")]
            public string? Password { get; init; }
            public string? Email { get; init; }
            public string? PhoneNumber { get; init; }
            public ICollection<string>? Roles { get; init; }
        }


        //jwt
        public record UserLoginDto
        {
            [Required(ErrorMessage = "User name is required")]
            public string? UserName { get; init; }
            [Required(ErrorMessage = "Password name is required")]
            public string? Password { get; init; }
        }

        //public record CompanyForCreationDto(string Name, string Address, string Country,
        //                             IEnumerable<EmployeeCreationDto> Employees);
        //public record CompanyForUpdateWithInsertChieldDto(string Name, string Address, string Country,
        //                            IEnumerable<EmployeeCreationDto> Employees);

        public record CompanyDto(Guid Id, string Name, string FullAddress);
        public record CompanyForCreationDto : CompanyWithChieldForManipulationDto;
        public record CompanyForUpdateWithInsertChieldDto: CompanyWithChieldForManipulationDto;
        public record CompanyForUpdateWithOutInsertChieldDto: CompanyWithoutchiledForManipulationDto;
        public record CompanyCreationDto : CompanyWithoutchiledForManipulationDto;
        public record EmployeeDto(Guid Id, string Name, int Age, string Position);   
        public record EmployeeCreationDto : EmployeeForManipulationDto;
        public record EmployeeForUpdateDto: EmployeeForManipulationDto;





    }
}
