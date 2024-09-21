﻿using LinkDev.IKEA.BLL.Models.Departments;
using LinkDev.IKEA.DAL.Entities.Department;
using LinkDev.IKEA.DAL.Persistance.Repositories.Departments;
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
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)//ASK CLR FOR CREATING OBJECT FROM CLASS IMPLEMENTING THE INTERFACE IDepartmentRepository
        {
            _departmentRepository = departmentRepository;
        }

        public IEnumerable<DepartmentToReturnDto> GetAllDepartments()
        {
            var departments = _departmentRepository.GetAllAsIQueryable().Select(department => new DepartmentToReturnDto()
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
            var department = _departmentRepository.Get(id);

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

            return _departmentRepository.Add(department);

            

           
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

            return _departmentRepository.Update(department);
        }

        public bool DeleteDepartment(int id)
        {
            var department = _departmentRepository.Get(id);
            
            if(department is { })
                return _departmentRepository.Delete(department) > 0;

            return false;
            
        }

      


        
    }
}
