using Demo_BLL.Interfaces;
using Demo_DAL.Contexts;
using Demo_DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_BLL.Repositories
{
    public class GenericRepository<T>:IGenericRepository<T> where T : class
    {
        private readonly MVCAppG02DbContext _dbcontext;

        public GenericRepository(MVCAppG02DbContext dbContext)
        {
            _dbcontext = dbContext;
        }
        public async Task<int> Add(T item)
        {
            await _dbcontext.Set<T>().AddAsync(item);
            return await _dbcontext.SaveChangesAsync();
        }

        public async Task<int> Delete(T item)
        {
            _dbcontext.Set<T>().Remove(item);
            return await _dbcontext.SaveChangesAsync();
        }

        public async Task<T> Get(int id)
           // => _dbcontext.Departments.Where(D => D.Id == id).AsNoTracking().FirstOrDefault();
           => await _dbcontext.Set<T>().FindAsync(id);
        public async Task<IEnumerable<T>> GetAll()
            => await _dbcontext.Set<T>().ToListAsync();

        public async Task<int> Update(T item)
        {
            _dbcontext.Set<T>().Update(item);
            return await _dbcontext.SaveChangesAsync();
        }
    }
}
