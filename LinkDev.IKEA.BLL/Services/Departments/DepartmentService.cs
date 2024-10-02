using LinkDev.IKEA.BLL.Models.Departments;
using LinkDev.IKEA.DAL.Entities.DepartmentEntity;
using LinkDev.IKEA.DAL.Persistance.Repositories.Departments;
using LinkDev.IKEA.DAL.Persistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Services.Departments
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IDepartmentRepository _departmentRepository;



        public DepartmentService(IUnitOfWork unitOfWork)//ASK CLR FOR CREATING OBJECT FROM CLASS IMPLEMENTING THE INTERFACE IDepartmentRepository
        {
            
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync()
        {
            var departmentRepo = _unitOfWork.DepartmentRepository;
            var departments = await departmentRepo
                .GetIQueryable()
                .Where(D => !D.IsDeleted)
                .Select(department => new DepartmentDto()
            {
                Id = department.Id,
                Code = department.Code,
                Name = department.Name,
                CreationDate = department.CreationDate
                
                
            }).AsNoTracking().ToListAsync();

            return departments;
        }


        public async Task<DepartmentDetailsDto?> GetDepartmentByIdAsync(int id)
        {
            var department = await _unitOfWork.DepartmentRepository.GetAsync(id);

            if(department is { })
            return new DepartmentDetailsDto()
            {
                Id             = department.Id,
                Code           = department.Code,
                Name           = department.Name,
                Description    = department.Description,
                CreationDate   = department.CreationDate,
                CreatedOn      = department.CreatedOn,
                CreatedBy      = department.CreatedBy,
                LastModifiedBy = department.LastModifiedBy,
                LastModifiedOn = department.LastModifiedOn
            };

            return null;

        }

        public async Task<int> CreateDepartmentAsync(CreatedDepartmentDto departmentDto)
        {
            var department = new Department()
            {
                
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                Description = departmentDto.Description,
                CreationDate = departmentDto.CreationDate,
                CreatedBy = 1,
                // //CreatedOn = DateTime.UtcNow,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow
            };

            _unitOfWork.DepartmentRepository.Add(department);

            return await _unitOfWork.CompleteAsync();




        }

        public async Task<int> UpdateDepartmentAsync(UpdatedDepartmentDto departmentDto)
        {
            var department = new Department()
            {
                Id = departmentDto.Id,
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                Description = departmentDto.Description,
                CreationDate = departmentDto.CreationDate,               
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow
            };

             _unitOfWork.DepartmentRepository.Update(department);

            return await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> DeleteDepartmentAsync(int id)
        {
            var departmentRepository = _unitOfWork.DepartmentRepository;

            var department = await departmentRepository.GetAsync(id);
            
            if(department is { })
                departmentRepository.Delete(department);

            return await _unitOfWork.CompleteAsync() > 0;
            
        }

      
    }
}
