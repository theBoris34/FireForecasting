using Microsoft.Extensions.DependencyInjection;
using FireForecasting.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using FireForecasting.DAL.Entityes.Departments;
using FireForecasting.DAL.Entityes.Incidents;

namespace FireForecasting.DAL
{
    public static class RepositoryRegistrator
    {
        public static IServiceCollection AddRepositoriesInDB(this IServiceCollection services) => services
            .AddTransient<IRepository<Department>, DepartmentRepository>()
            .AddTransient<IRepository<Division>, DivisionRepository>()
            .AddTransient<IRepository<Employee>, EmployeeRepository>()
            .AddTransient<IRepository<Fire>, FireRepository>()
            ;
    }
}
