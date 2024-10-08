﻿using LinkDev.IKEA.DAL.Entities;
using LinkDev.IKEA.DAL.Entities.DepartmentEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistance.Repositories._Generic
{
    public interface IGenericRepository<T> where T : ModelBase
    {
        Task<T?> GetAsync(int id);

        Task<IEnumerable<T>> GetAllAsync(bool withAsNoTracking = false);

        IQueryable<T> GetIQueryable();


        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
