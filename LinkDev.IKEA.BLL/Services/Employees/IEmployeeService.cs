using LinkDev.IKEA.BLL.Models.Employees;

namespace LinkDev.IKEA.BLL.Services.Employees
{
    public interface IEmployeeService
    {

        IEnumerable<EmployeeDto> GetEmployees(string search);


        DetailsEmployeeDto? GetEmployeeById(int id);

        int CreateEmployee(CreatedEmployeeDto EmployeeDto);

        int UpdateEmployee(UpdatedEmployeeDto Employee);

        bool DeleteEmployee(int id);

    }
}
