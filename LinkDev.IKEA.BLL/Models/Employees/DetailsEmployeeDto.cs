using LinkDev.IKEA.DAL.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Models.Employees
{
    public class DetailsEmployeeDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        
        public int? Age { get; set; }

        public string Code { get; set; } = null!;

        public string? Description { get; set; }

        //[RegularExpression("@^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
        //                 ErrorMessage = "Address must be like 123-Street-City-Country")]
        public string Address { get; set; } = null!;

        [Display(Name = "Creation Date")]
        public DateOnly CreationDate { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime LastModifiedOn { get; set; }

        public int LastModifiedBy { get; set; }

        public int CreatedBy { get; set; }
       


    


        public decimal Salary { get; set; }


        public bool IsActive { get; set; }

        [Display(Name = "Phone Number")]
        [Phone]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Hiring Date")]
        public DateOnly HiringDate { get; set; }

        public string? Email { get; set; }


        public Gender Gender { get; set; } 

        public EmployeeType EmployeeType { get; set; }

        public int? DepartmentId { get; set; }

        public string Department { get; set; } = null!;
    }
}
