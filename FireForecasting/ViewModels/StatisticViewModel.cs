using FireForecasting.DAL.Entityes.Departments;
using FireForecasting.DAL.Entityes.Incidents;
using FireForecasting.Interfaces;
using FireForecasting.Services.Intarface;
using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FireForecasting.ViewModels
{
    class StatisticViewModel : ViewModel
    {
        private IRepository<Employee> _EmployeeRepository;
        private IRepository<Division> _DivisionRepository;
        private IRepository<Fire> _FireRepository;
        private IFireService _FireService;

        private int _EmployeeCount;
        public int EmployeeCount { get => _EmployeeCount; private set => Set(ref _EmployeeCount, value); }



        #region Команда вычисления статистики

        private ICommand _ComputeStatisticCommand;

        public ICommand ComputeStatisticCommand => _ComputeStatisticCommand
            ??= new LambdaCommandAsync(OnComputeStatisticCommandExecuted, CanComputeStatisticCommandExecuted);

        private bool CanComputeStatisticCommandExecuted() => true;

        private async Task OnComputeStatisticCommandExecuted()
        {
            EmployeeCount = await _EmployeeRepository.Items.CountAsync();

            var fires = _FireRepository.Items;

            var fires1 = await fires.GroupBy(f => f.Division)
                              .Select(fire_complited => new { Division = fire_complited.Key, Count = fire_complited.Count() })
                              .Take(5)
                              .ToArrayAsync();
        }

        #endregion

        public StatisticViewModel(IRepository<Employee> employeeRepository, IRepository<Division> divisionRepository, IRepository<Fire> fireRepository)
        {
            _EmployeeRepository = employeeRepository;
            _DivisionRepository = divisionRepository;
            _FireRepository = fireRepository;
        }
    }
}
