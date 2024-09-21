using LinkDev.IKEA.DAL.Entities.Department;
using LinkDev.IKEA.DAL.Entities.Employee;
using LinkDev.IKEA.DAL.Persistance.Data;
using LinkDev.IKEA.DAL.Persistance.Repositories._Generic;
using LinkDev.IKEA.DAL.Persistance.Repositories.Departments;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistance.Repositories.Employees
{
    public class EmployeeRepository : GenericRepository<Employee>,IEmployeeRepository
    {

        public EmployeeRepository(ApplicationDbContext dbContext):base(dbContext)//ASK CLR OBJECT FROM CLASS DBCONTEXT
        {
            
        }

    }
}
