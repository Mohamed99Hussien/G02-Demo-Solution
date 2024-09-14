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
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(MVCAppG02DbContext dbContext):base(dbContext)
        {
                
        }
    }
}
