using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using FireForecasting.DAL.Entityes.Departments;
using FireForecasting.Interfaces;
using FireForecasting.Services.Intarface;
using MathCore.WPF.Commands;
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
            CurrentModel = new EmployeeViewModel(_EmployeeRepository);
        } 

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="EmployeeRepository"></param>
        /// <param name="DivisionRepository"></param>
        /// <param name="FireService"></param>
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
