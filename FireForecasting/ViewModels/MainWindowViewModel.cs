using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FireForecasting.DAL.Entityes.Departments;
using FireForecasting.Interfaces;
using MathCore.WPF.ViewModels;

namespace FireForecasting.ViewModels
{
    class MainWindowViewModel:ViewModel
    {
        private string _Title = "Главное окно программы!";
        private readonly IRepository<Employee> _employeeRepository;

        public string Title { get=>_Title; set => Set(ref _Title,value); }

        public MainWindowViewModel(IRepository<Employee> EmployeeRepository)
        {
            _employeeRepository = EmployeeRepository;

            var employee = EmployeeRepository.Items.Take(50).ToArray();
        }
    }
}
