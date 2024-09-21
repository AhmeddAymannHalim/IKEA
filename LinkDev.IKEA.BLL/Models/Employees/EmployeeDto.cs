using LinkDev.IKEA.DAL.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Models.Employees
{
    public class EmployeeDto


    {
        #region General

        public int Id { get; set; }

        public string Name { get; set; } = null!;


        public int? Age { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }


        public bool IsActive { get; set; }

        [EmailAddress]
        public string? Email { get; set; }


        public string Gender { get; set; } = null!;

        public string EmployeeType { get; set; } = null!; 
        #endregion

        #region Administrator

        public DateTime CreatedOn { get; set; }

        public DateTime LastModifiedOn { get; set; }

        public int LastModifiedBy { get; set; }

        public int CreatedBy { get; set; } 
        #endregion
    }
}
