using FireForecasting.DAL.Entityes.Departments;
using FireForecasting.DAL.Entityes.Incidents;
using FireForecasting.Interfaces;
using FireForecasting.Models;
using FireForecasting.Services.Intarface;
using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FireForecasting.ViewModels
{
    class StatisticViewModel : ViewModel
    {
        private readonly IUserDialog _UserDialug;
        private readonly IRepository<Employee> _EmployeeRepository;
        private readonly IRepository<Division> _DivisionRepository;
        private readonly IRepository<Fire> _FireRepository;

        public ObservableCollection<ManyFiresDivisionInfo> ManyFiresDivision { get; } = new ObservableCollection<ManyFiresDivisionInfo>();

        #region Команда вычисления статистики

        private ICommand _ComputeStatisticCommand;

        public ICommand ComputeStatisticCommand => _ComputeStatisticCommand
            ??= new LambdaCommandAsync(OnComputeStatisticCommandExecuted);

        /// <summary> Логика вычисления подразделения с наибольшим количеством пожаров. </summary>
        private async Task OnComputeStatisticCommandExecuted()
        {
            await ComputeManyFiresDivisionAsync();
        }

        private async Task ComputeManyFiresDivisionAsync()
        {
            var manyFiresDivision_query = _FireRepository.Items
                .GroupBy(f => f.Division.Id)
                .Select(fires => new { DivisionId = fires.Key, Count = fires.Count()})
                .OrderByDescending(fires => fires.Count)
                .Take(10)
                .Join(_DivisionRepository.Items, 
                    f => f.DivisionId, 
                    d => d.Id, 
                    (f, d) => new ManyFiresDivisionInfo { Division = d, FiresCount = f.Count});


            ManyFiresDivision.Clear();
            foreach (var manyFiresDivision in await manyFiresDivision_query.ToArrayAsync())
                ManyFiresDivision.Add(manyFiresDivision);
        }

        #endregion

        public StatisticViewModel( IRepository<Employee> employeeRepository, IRepository<Division> divisionRepository, IRepository<Fire> fireRepository, IUserDialog UserDialog)
        {
            _UserDialug = UserDialog;
            _EmployeeRepository = employeeRepository;
            _DivisionRepository = divisionRepository;
            _FireRepository = fireRepository;
        }
    }
}
