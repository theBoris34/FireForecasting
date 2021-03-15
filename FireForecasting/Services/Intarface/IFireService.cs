using FireForecasting.DAL.Entityes.Departments;
using FireForecasting.DAL.Entityes.Incidents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireForecasting.Services.Intarface
{
    interface IFireService
    {
        IEnumerable<Fire> Fires { get; }

        Task<Fire> RegisterFireAsync(string adress, Employee employee, Division division, decimal costOfDamage);
    }
}
