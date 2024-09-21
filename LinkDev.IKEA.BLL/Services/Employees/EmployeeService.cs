using LinkDev.IKEA.BLL.Models.Employees;
using LinkDev.IKEA.DAL.Common.Enums;
using LinkDev.IKEA.DAL.Entities.Employee;
using LinkDev.IKEA.DAL.Persistance.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return _employeeRepository.GetAllAsIQueryable().Select(EmployeeDto => new EmployeeDto
            {
                Name = EmployeeDto.Name,
                Age = EmployeeDto.Age,
                IsActive = EmployeeDto.IsActive,
                Email = EmployeeDto.Email,
                Gender = nameof(EmployeeDto.Gender),
                EmployeeType = nameof(EmployeeDto.EmployeeType),

            });

        }

        public DetailsEmployeeDto? GetEmployeeById(int id)
        {
            var employee = _employeeRepository.Get(id);

            if(employee is { })

             return new DetailsEmployeeDto()
            {
                Name = employee.Name,
                Age = employee.Age,
                Address = employee.Address,
                IsActive = employee.IsActive,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                HiringDate = employee.HiringDate,
                Gender = nameof(employee.Gender),
                EmployeeType = nameof(employee.EmployeeType),
                
            };

            return null;
        }


        public int CreateEmployee(CreatedEmployeeDto EmployeeDto)
        {
            var employee = new Employee()
            {
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
