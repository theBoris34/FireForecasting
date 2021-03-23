using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using FireForecasting.DAL.Entityes.Departments;
using FireForecasting.DAL.Entityes.Incidents;
using FireForecasting.Interfaces;
using FireForecasting.Services.Intarface;
using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;

namespace FireForecasting.ViewModels
{
    class MainWindowViewModel:ViewModel
    {
        private string _Title = "Главное окно программы!";
        private readonly IUserDialog _UserDialog;
        private readonly IRepository<Employee> _EmployeeRepository;
        private readonly IRepository<Division> _DivisionRepository;
        private readonly IRepository<Fire> _FireRepository;
        private readonly IFireService _FireService;

        public string Title { get=>_Title; set => Set(ref _Title,value); }

        /// <summary> Текущая дочерняямодель представления </summary>/// 
        private ViewModel _CurrentModel;
        public ViewModel CurrentModel { get => _CurrentModel; private set => Set(ref _CurrentModel, value); }

        #region Команда отображения представления сотрудников
        
        private ICommand _ShowEmployeeViewCommand;

        public ICommand ShowEmployeeViewCommand => _ShowEmployeeViewCommand
            ??= new LambdaCommand(OnShowEmployeeViewCommandExecuted, CanShowEmployeeViewCommandExecuted);

        private bool CanShowEmployeeViewCommandExecuted() => true;

        private void OnShowEmployeeViewCommandExecuted()
        {
            CurrentModel = new EmployeeViewModel(_EmployeeRepository, _UserDialog);
        }

        #endregion

        #region Команда отображения представления статистики

        private ICommand _ShowStatisticViewCommand;

        public ICommand ShowStatisticViewCommand => _ShowStatisticViewCommand
            ??= new LambdaCommand(OnShowStatisticViewCommandExecuted, CanShowStatisticViewCommandExecuted);

        private bool CanShowStatisticViewCommandExecuted() => true;

        private void OnShowStatisticViewCommandExecuted()
        {
            CurrentModel = new StatisticViewModel(_EmployeeRepository, _DivisionRepository, _FireRepository, _UserDialog);
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="EmployeeRepository"></param>
        /// <param name="DivisionRepository"></param>
        /// <param name="FireService"></param>
        public MainWindowViewModel(
            IUserDialog UserDialog,
            IRepository<Employee> EmployeeRepository,
            IRepository<Division> DivisionRepository,
            IRepository<Fire> FireRepository,
            IFireService FireService)
        {
            _UserDialog = UserDialog;
            _EmployeeRepository = EmployeeRepository;
            _DivisionRepository = DivisionRepository;
            _FireRepository = FireRepository;
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
