using LinkDev.IKEA.DAL.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Models.Employees
{
    public class CreatedEmployeeDto
    {
        public int Id { get; set; }
        //[Required]
        [MaxLength(50,ErrorMessage ="Max Length Of Name is 50 chars")]
        [MinLength(5,ErrorMessage ="Min Length Of Name is 5 chars")]
        public string Name { get; set; } = null!;

        [Range(22,30)]
        public int? Age { get; set; }

       
        public string Address { get; set; } = null!;

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Display(Name ="Is Active")]
        public bool IsActive { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Display(Name ="Phone Number")]
        [Phone]
        public string? PhoneNumber { get; set; }

        [Display(Name ="Hiring Date")]
        public DateOnly HiringDate { get; set; }

        public Gender Gender { get; set; }

        public EmployeeType EmployeeType { get; set; }
    }
}
