using Demo_BLL.Interfaces;
using Demo_DAL.Contexts;
using Demo_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly MVCAppG02DbContext _dbContext;

        public EmployeeRepository(MVCAppG02DbContext dbContext):base(dbContext) 
        {
            _dbContext = dbContext;
        }
        public IQueryable<Employee> GetEmployeesByDepartmentName(string departmentName)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Employee> SearchEmployeesByName(string name)
           => _dbContext.Employees.Where(E => E.Name.Contains(name));
        
    }
}
