using FireForecasting.DAL.Context;
using FireForecasting.DAL.Entityes.Departments;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FireForecasting.DAL.Repositories
{
    class FireTruckRepository:DbRepository<FireTruck>
    {
        public override IQueryable<FireTruck> Items => base.Items
            .Include(item => item.Division.Department);

        public FireTruckRepository(DepartmentDB departmentDB):base(departmentDB) { }
    }
}
