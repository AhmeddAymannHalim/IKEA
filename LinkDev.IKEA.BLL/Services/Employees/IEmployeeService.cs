using LinkDev.IKEA.BLL.Models.Employees;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Services.Employees
{
    public interface IEmployeeService
    {

        IEnumerable<EmployeeDto> GetAllEmployees();


        DetailsEmployeeDto? GetEmployeeById(int id);

        int CreateEmployee(CreatedEmployeeDto EmployeeDto);

        int UpdateEmployee(UpdatedEmployeeDto Employee);

        bool DeleteEmployee(int id);

    }
}
