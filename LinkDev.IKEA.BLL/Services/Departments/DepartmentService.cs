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

        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = _unitOfWork.DepartmentRepository
                .GetIQueryable()
                .Where(D => !D.IsDeleted)
                .Select(department => new DepartmentDto()
            {
                Id = department.Id,
                Code = department.Code,
                Name = department.Name,
                CreationDate = department.CreationDate
                
                
            }).AsNoTracking().ToList();

            return departments;
        }


        public DepartmentDetailsDto? GetDepartmentById(int id)
        {
            var department = _unitOfWork.DepartmentRepository.Get(id);

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

        public int CreateDepartment(CreatedDepartmentDto departmentDto)
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

            return _unitOfWork.Complete();




        }

        public int UpdateDepartment(UpdatedDepartmentDto departmentDto)
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

            return _unitOfWork.Complete();
        }

        public bool DeleteDepartment(int id)
        {
            var departmentRepository = _unitOfWork.DepartmentRepository;

            var department = departmentRepository.Get(id);
            
            if(department is { })
                departmentRepository.Delete(department);

            return _unitOfWork.Complete() > 0;
            
        }

      


        
    }
}
