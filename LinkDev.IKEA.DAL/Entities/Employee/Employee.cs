using LinkDev.IKEA.DAL.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Entities.Employee
{
    public class Employee : ModelBase
    {

        public string Name { get; set; } = null!;

       
        public int? Age { get; set; }

       
        public string Address { get; set; } = null!;

        
        public decimal Salary { get; set; }

        
        public bool IsActive { get; set; }

       
        public string? Email { get; set; }

      
        public string? PhoneNumber { get; set; }

        
        public DateOnly HiringDate { get; set; }

        public Gender Gender { get; set; }

        public EmployeeType EmployeeType { get; set; }
    }
}
