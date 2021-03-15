using FireForecasting.DAL.Context;
using FireForecasting.DAL.Entityes.Incidents;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FireForecasting.DAL
{
    class FireRepository : DbRepository<Fire>
    {
        public override IQueryable<Fire> Items => base.Items
            .Include(item => item.Division)
            .Include(item => item.Employee);

        public FireRepository(DepartmentDB departmentDB) : base(departmentDB) { }
    }
}
