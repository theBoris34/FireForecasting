using FireForecasting.DAL.Context;
using FireForecasting.DAL.Entityes.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FireForecasting.DAL
{
    class RegionRepository:DbRepository<Region>
    {
        public override IQueryable<Region> Items => base.Items;

        public RegionRepository(DepartmentDB departmentDB):base(departmentDB)
        {

        }
    }
}
