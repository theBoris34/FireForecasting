using FireForecasting.DAL.Context;
using FireForecasting.DAL.Entityes.Departments;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FireForecasting.DAL
{
    class DutyShiftRepository : DbRepository<DutyShift>
    {
        public override IQueryable<DutyShift> Items => base.Items
            .Include(item => item.Division.Department);

        public DutyShiftRepository(DepartmentDB departmentDB):base(departmentDB) { }
    }
}
