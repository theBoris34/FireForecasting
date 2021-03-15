using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FireForecasting.DAL.Entityes.Departments;
using FireForecasting.Interfaces;
using FireForecasting.Services.Intarface;
using MathCore.WPF.ViewModels;

namespace FireForecasting.ViewModels
{
    class MainWindowViewModel:ViewModel
    {
        private string _Title = "Главное окно программы!";
        private readonly IRepository<Employee> _EmployeeRepository;
        private readonly IRepository<Division> _DivisionRepository;
        private readonly IFireService _FireService;

        public string Title { get=>_Title; set => Set(ref _Title,value); }

        public MainWindowViewModel(
            IRepository<Employee> EmployeeRepository,
            IRepository<Division> DivisionRepository,
            IFireService FireService)
        {
            _EmployeeRepository = EmployeeRepository;
            _DivisionRepository = DivisionRepository;
            _FireService = FireService;

            //Test();
        }

        private async void Test()
        {
            var fires_count = _FireService.Fires.Count();
            var Rnd = new Random();
            var employee = await _EmployeeRepository.GetAsync(5);
            var division = employee.Division;
            string adress = new HashCode().ToString();
            decimal costOfDamage = Rnd.Next(10_000, 500_000_000);

            var fire = _FireService.RegisterFireAsync(adress, employee, division, costOfDamage);

            var fire_count2 = _FireService.Fires.Count();
        }
    }
}
