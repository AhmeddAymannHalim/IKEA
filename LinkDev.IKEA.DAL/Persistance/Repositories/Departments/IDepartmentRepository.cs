﻿using LinkDev.IKEA.DAL.Entities.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistance.Repositories.Departments
{
    public interface IDepartmentRepository
    {
        Department? Get(int id);

        IEnumerable<Department> GetAll(bool withAsNoTracking = false);

        IQueryable<Department> GetAllAsIQueryable();

      

        int Add(Department entity);

        int Update(Department entity);

        int Delete(Department entity);

    }
}
