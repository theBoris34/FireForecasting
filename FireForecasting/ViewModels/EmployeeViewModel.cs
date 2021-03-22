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

        public IEnumerable<Employee> Employees => _EmployeeRepository.Items;

        public EmployeeViewModel()
        {
            if (!App.IsDesignTime)
                throw new InvalidOperationException("Данный конструктор не предназначен для использования вне дизайнера!");
        }

        public EmployeeViewModel(IRepository<Employee> EmployeeRepository)
        {
            _EmployeeRepository = EmployeeRepository;
        }
    }
}
