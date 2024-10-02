using LinkDev.IKEA.BLL.Common.Services.Attachments;
using LinkDev.IKEA.BLL.Models.Employees;
using LinkDev.IKEA.DAL.Entities.EmployeeEntity;
using LinkDev.IKEA.DAL.Persistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LinkDev.IKEA.BLL.Services.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttachmentService _attachmentService;

        //private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IUnitOfWork unitOfWork,
                               IAttachmentService attachmentService)//ASK CLR FOR CREATING OBJECT FROM CLASS IMPLEMENT IEmployeeRepository
        {
            
            _unitOfWork = unitOfWork;
            _attachmentService = attachmentService;
        }


        public IEnumerable<EmployeeDto> GetEmployees(string search)
        {
            return _unitOfWork.EmployeeRepository.GetIQueryable()
                                      .Where(E => !E.IsDeleted && (search.IsNullOrEmpty() || E.Name.ToLower().Contains(search.ToLower())))
                                      .Include(E => E.Department)
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
                Department = EmployeeDto.Department.Name,
                Image = EmployeeDto.Image
               
                 

            }).ToList();

        }

        public DetailsEmployeeDto? GetEmployeeById(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.Get(id);

            if (employee is { })

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
                    Image = employee.Image
                   



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
                DepartmentId = EmployeeDto.DepartmentId,
                CreatedBy = 1,
                CreatedOn = DateTime.UtcNow,
                LastModifiedBy =1,
                LastModifiedOn = DateTime.UtcNow,
                


            };

            if(EmployeeDto.Image is not null)
            {
               employee.Image = _attachmentService.Upload(EmployeeDto.Image,"images");

            }

             _unitOfWork.EmployeeRepository.Add(employee);

            return _unitOfWork.Complete();

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
                DepartmentId = EmployeeDto.DepartmentId,
                CreatedBy = 1,
                //CreatedOn = DateTime.UtcNow
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow

            };

            _unitOfWork.EmployeeRepository.Update(employee);

            return _unitOfWork.Complete();
        }



        public bool DeleteEmployee(int id)
        {
            var employeeRepository = _unitOfWork.EmployeeRepository;

            var employee = employeeRepository.Get(id);

            if(employee is { })
                 employeeRepository.Delete(employee) ;




            return _unitOfWork.Complete() > 0;
        }

      
      
       
    }
}
