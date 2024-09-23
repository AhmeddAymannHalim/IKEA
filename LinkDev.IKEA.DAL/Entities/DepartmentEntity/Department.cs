using LinkDev.IKEA.DAL.Entities.EmployeeEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Entities.DepartmentEntity
{
    public class Department : ModelBase
    {
        public string Name { get; set; } = null!;

        public string Code { get; set; } = null!;

        public string? Description { get; set; }

        public DateOnly CreationDate { get; set; }

        //Navigational Property [Many]
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>(); 
    }
}
