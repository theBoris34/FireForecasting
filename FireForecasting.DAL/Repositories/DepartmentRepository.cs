using FireForecasting.DAL.Context;
using FireForecasting.DAL.Entityes.Departments;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FireForecasting.DAL
{
    class DepartmentRepository : DbRepository<Department>
    {
        public override IQueryable<Department> Items => base.Items
            .Include(item => item.Divisions);

        public DepartmentRepository(DepartmentDB departmentDB) : base(departmentDB) { }
    }
}
