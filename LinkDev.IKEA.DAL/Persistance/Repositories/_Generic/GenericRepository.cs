using LinkDev.IKEA.DAL.Entities;
using LinkDev.IKEA.DAL.Entities.Department;
using LinkDev.IKEA.DAL.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistance.Repositories._Generic
{
    public class GenericRepository<T> where T :ModelBase , IGenericRepository<T>
    {
        private protected readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)//ASK CLR TO MAKE OBJECT FROM APPLICATIONDBCONTEXT
        {
            _dbContext = dbContext;
        }

        public IEnumerable<T> GetAll(bool withAsNoTracking = false)
        {
            if (withAsNoTracking)
                return _dbContext.Set<T>().Where(X => !X.IsDeleted).AsNoTracking().ToList();

            return _dbContext.Set<T>().Where(X => !X.IsDeleted).ToList();
        }


        public IQueryable<T> GetIQueryable()
        {
            return _dbContext.Set<T>();
        }


        public T? Get(int id)
        {
            // var T = _dbContext.Ts.Local.FirstOrDefault(D => D.Id == id);
            // return T;

            return _dbContext.Set<T>().Find(id);
        }

        public int Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            return _dbContext.SaveChanges();
        }

        public int Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            return _dbContext.SaveChanges();
        }

        public int Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return _dbContext.SaveChanges();
        }

    }
}
