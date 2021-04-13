using FireForecasting.DAL.Entityes.Departments;
using FireForecasting.DAL.Entityes.Incidents;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireForecasting.DAL.Context
{
    public class DepartmentDB : DbContext
    {
        public DbSet<Fire> Fires { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DepartmentDB(DbContextOptions<DepartmentDB> options):base(options) { }
    }
}
