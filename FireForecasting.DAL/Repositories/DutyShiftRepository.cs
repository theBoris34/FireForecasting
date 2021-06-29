using FireForecasting.DAL.Context;
using FireForecasting.DAL.Entityes.Departments;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FireForecasting.DAL
{
    class DutyShiftRepository : DbRepository<DutyShift>
    {
        public override IQueryable<DutyShift> Items => base.Items
            .Include(item => item.Division.Department);

        public DutyShiftRepository(DepartmentDB departmentDB):base(departmentDB) { }
    }
}
