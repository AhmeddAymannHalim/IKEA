using LinkDev.IKEA.BLL.Models.Employees;

namespace LinkDev.IKEA.BLL.Services.Employees
{
    public interface IEmployeeService
    {

        Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(string search);

        Task<DetailsEmployeeDto?> GetEmployeeByIdAsync(int id);

        Task<int> CreateEmployeeAsync(CreatedEmployeeDto EmployeeDto);

        Task<int> UpdateEmployeeAsync(UpdatedEmployeeDto Employee);

        Task<bool> DeleteEmployeeAsync(int id);

    }
}
