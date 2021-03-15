using FireForecasting.DAL.Entityes.Departments;
using FireForecasting.DAL.Entityes.Incidents;
using FireForecasting.Interfaces;
using FireForecasting.Services.Intarface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireForecasting.Services
{
    class FireService : IFireService
    {
        private readonly IRepository<Fire> _Fires;
        private readonly IRepository<Department> _Departments;
        private readonly IRepository<Division> _Divisions;
        private readonly IRepository<Employee> _Employees;

        public IEnumerable<Fire> Fires => _Fires.Items;

        public FireService(
            IRepository<Fire> Fires, 
            IRepository<Department> Departments, 
            IRepository<Division> Divisions,
            IRepository<Employee> Employees)
        {
            _Fires = Fires;
            _Departments = Departments;
            _Divisions = Divisions;
            _Employees = Employees;
        }

        public async Task<Fire> RegisterFireAsync(
            string adress, 
            Employee employee, 
            Division division, 
            decimal costOfDamage) 
        {
            var fire = new Fire
            {
                CostOfDamage = costOfDamage,
                Adress = adress,
                Employee = employee,
                Division = division
            };

            return await _Fires.AddAsync(fire);
        }

    }
}
