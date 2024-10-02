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


        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(string search)
        {
            return await _unitOfWork.EmployeeRepository.GetIQueryable()
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
               
                 

            }).ToListAsync();

        }

        public async Task<DetailsEmployeeDto?> GetEmployeeByIdAsync(int id)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetAsync(id);

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


        public async Task<int> CreateEmployeeAsync(CreatedEmployeeDto EmployeeDto)
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
               employee.Image = await _attachmentService.UploadFileAsync(EmployeeDto.Image,"images");

            }

             _unitOfWork.EmployeeRepository.Add(employee);

            return await _unitOfWork.CompleteAsync();

        }


        public async Task<int> UpdateEmployeeAsync(UpdatedEmployeeDto EmployeeDto)
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

            return await _unitOfWork.CompleteAsync();
        }



        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employeeRepository = _unitOfWork.EmployeeRepository;

            var employee = await employeeRepository.GetAsync(id);

            if(employee is { })
                 employeeRepository.Delete(employee) ;




            return await _unitOfWork.CompleteAsync() > 0;
        }

      
      
       
    }
}
