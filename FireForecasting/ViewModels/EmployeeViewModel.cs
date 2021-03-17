using FireForecasting.DAL.Entityes.Departments;
using FireForecasting.Interfaces;
using MathCore.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireForecasting.ViewModels
{
    class EmployeeViewModel : ViewModel
    {
        private readonly IRepository<Employee> _EmployeeRepository;

        public EmployeeViewModel(IRepository<Employee> EmployeeRepository)
        {
            _EmployeeRepository = EmployeeRepository;
        }
    }
}
