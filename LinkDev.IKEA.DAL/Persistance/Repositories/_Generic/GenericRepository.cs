using LinkDev.IKEA.DAL.Entities;
using LinkDev.IKEA.DAL.Entities.DepartmentEntity;
using LinkDev.IKEA.DAL.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistance.Repositories._Generic
{
    public class GenericRepository<T> where T : ModelBase
    {
        private protected readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)//ASK CLR TO MAKE OBJECT FROM APPLICATIONDBCONTEXT
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<T>> GetAllAsync(bool withAsNoTracking = false)
        {
            if (withAsNoTracking)
                return await _dbContext.Set<T>().Where(X => !X.IsDeleted).AsNoTracking().ToListAsync();

            return await _dbContext.Set<T>().Where(X => !X.IsDeleted).ToListAsync();
        }


        public IQueryable<T> GetIQueryable()
        {
            return _dbContext.Set<T>();
        }


        public async Task<T?> GetAsync(int id)
        {
            // var T = _dbContext.Ts.Local.FirstOrDefault(D => D.Id == id);
            // return T;

            return await _dbContext.Set<T>().FindAsync(id);
        }


        public void Add(T entity)
                =>  _dbContext.Set<T>().Add(entity);


        public void Update(T entity)
                => _dbContext.Set<T>().Update(entity);


        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            _dbContext.Set<T>().Update(entity);

        }
    }
}
