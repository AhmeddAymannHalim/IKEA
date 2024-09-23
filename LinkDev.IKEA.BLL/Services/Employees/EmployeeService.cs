using LinkDev.IKEA.BLL.Models.Employees;
using LinkDev.IKEA.DAL.Entities.Employee;
using LinkDev.IKEA.DAL.Persistance.Repositories.Employees;

namespace LinkDev.IKEA.BLL.Services.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

       public EmployeeService(IEmployeeRepository employeeRepository)//ASK CLR FOR CREATING OBJECT FROM CLASS IMPLEMENT IEmployeeRepository
        {
            _employeeRepository = employeeRepository;
        }


        public IEnumerable<EmployeeDto> GetAllEmployees()
        {
            return _employeeRepository.GetAllAsIQueryable()
                                      .Where(E => !E.IsDeleted)
                                      .Select(EmployeeDto => new EmployeeDto
            {
                Id= EmployeeDto.Id,
                Name = EmployeeDto.Name,
                Age = EmployeeDto.Age,
                Salary = EmployeeDto.Salary,
                IsActive = EmployeeDto.IsActive,
                Email = EmployeeDto.Email,
                Gender = EmployeeDto.Gender.ToString(),
                EmployeeType = EmployeeDto.EmployeeType.ToString(),

            }).ToList();

        }

        public DetailsEmployeeDto? GetEmployeeById(int id)
        {
            var employee = _employeeRepository.Get(id);

            if(employee is { })

             return new DetailsEmployeeDto()
            {
                Name = employee.Name,
                Age = employee.Age,
                Salary = employee.Salary,
                Address = employee.Address,
                IsActive = employee.IsActive,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                HiringDate = employee.HiringDate,
                Gender = employee.Gender,
                EmployeeType = employee.EmployeeType,
                
            };

            return null;
        }


        public int CreateEmployee(CreatedEmployeeDto EmployeeDto)
        {
            var employee = new Employee()
            {
                Name = EmployeeDto.Name,
                Age = EmployeeDto.Age,
                Salary = EmployeeDto.Salary,
                Address = EmployeeDto.Address,
                IsActive = EmployeeDto.IsActive,
                Email = EmployeeDto.Email,
                PhoneNumber = EmployeeDto.PhoneNumber,
                HiringDate = EmployeeDto.HiringDate,
                Gender = EmployeeDto.Gender,
                EmployeeType = EmployeeDto.EmployeeType,
                CreatedBy = 1,
                //CreatedOn = DateTime.UtcNow
                LastModifiedBy =1,
                LastModifiedOn = DateTime.UtcNow

            };

            return _employeeRepository.Add(employee);

        }


        public int UpdateEmployee(UpdatedEmployeeDto EmployeeDto)
        {
            var employee = new Employee()
            {
                Id = EmployeeDto.Id, 
                Name = EmployeeDto.Name,
                Age = EmployeeDto.Age,
                Address = EmployeeDto.Address,
                IsActive = EmployeeDto.IsActive,
                Email = EmployeeDto.Email,
                PhoneNumber = EmployeeDto.PhoneNumber,
                HiringDate = EmployeeDto.HiringDate,
                Gender = EmployeeDto.Gender,
                EmployeeType = EmployeeDto.EmployeeType,
                CreatedBy = 1,
                //CreatedOn = DateTime.UtcNow
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow

            };

            return _employeeRepository.Update(employee);
        }



        public bool DeleteEmployee(int id)
        {
            var employee = _employeeRepository.Get(id);

            if(employee is { })
                return _employeeRepository.Delete(employee) > 0;


            return false;
        }

      
      
       
    }
}
