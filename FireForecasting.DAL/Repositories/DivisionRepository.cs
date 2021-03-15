using FireForecasting.DAL.Context;
using FireForecasting.DAL.Entityes.Departments;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FireForecasting.DAL
{
    class DivisionRepository : DbRepository<Division>
    {
        public override IQueryable<Division> Items => base.Items.Include(item => item.Department);

        public DivisionRepository(DepartmentDB departmentDB) : base(departmentDB) { }
    }
}
