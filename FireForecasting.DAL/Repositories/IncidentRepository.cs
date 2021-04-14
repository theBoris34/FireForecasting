using FireForecasting.DAL.Context;
using FireForecasting.DAL.Entityes.Incidents;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FireForecasting.DAL
{
    class IncidentRepository : DbRepository<Incident>
    {
        public override IQueryable<Incident> Items => base.Items;
        public IncidentRepository(DepartmentDB departmentDB) : base(departmentDB) { }
    }
}
