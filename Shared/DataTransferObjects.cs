﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class DataTransferObjects
    {
        public record CompanyDto(Guid Id, string Name, string FullAddress);
        public record EmployeeDto(Guid Id, string Name, int Age, string Position);

    }
}
