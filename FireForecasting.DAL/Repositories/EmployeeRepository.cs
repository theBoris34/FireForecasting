using FireForecasting.DAL.Context;
using FireForecasting.DAL.Entityes.Departments;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FireForecasting.DAL
{
    class EmployeeRepository : DbRepository<Employee>
    {
        public override IQueryable<Employee> Items => base.Items
            .Include(item => item.Division.Department);

        public EmployeeRepository(DepartmentDB departmentDB) : base(departmentDB) { }
    }
}
