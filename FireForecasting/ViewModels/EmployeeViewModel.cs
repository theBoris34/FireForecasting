using FireForecasting.DAL.Entityes.Departments;
using FireForecasting.Interfaces;
using FireForecasting.Services.Intarface;
using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace FireForecasting.ViewModels
{
    class EmployeeViewModel : ViewModel
    {
        private readonly IRepository<Employee> _EmployeeRepository;
        private readonly IUserDialog _UserDialog;
        private ObservableCollection<Employee> _EmployeesCollection;
        public ObservableCollection<Employee> EmployeesCollection
        {
            get => _EmployeesCollection; 
            set
            {
                if (Set(ref _EmployeesCollection, value))
                {
                    _EmployeeViewSource.Source = value;
                    _EmployeeViewSource.View.Refresh();
                    OnPropertyChanged(nameof(EmployeesView));
                }
            }
        }



        #region EmployeeFilter - Искомое слово
        //Фильтр
        //1. Свойство с текстом фильтра.
        //2. CollectionViewSource ему присвоить источник данных.
        //3. Свойство CollectionViewSource.View
        //4. Обработчик события фильтра
        //5. Соответсвие элемента фильтру, если нет выбрасываем.

        private string _EmployeeFilter;

        public string EmployeeFilter 
        { 
            get => _EmployeeFilter; 
            set
            {
                if (Set(ref _EmployeeFilter, value))
                    _EmployeeViewSource.View.Refresh();
            }
        }

        #endregion

        private readonly CollectionViewSource _EmployeeViewSource;

        public ICollectionView EmployeesView => _EmployeeViewSource.View;


        public IEnumerable<Employee> Employees => _EmployeeRepository.Items;

        /// <summary> Выбранный сотрудник </summary>
        private Employee _SelectedEmployee;

        public Employee SelectedEmployee
        {
            get => _SelectedEmployee;
            set => Set(ref _SelectedEmployee, value);
        }

        #region Команда добавления нового сотрудника

        private ICommand _AddNewEmployeeCommand;

        public ICommand AddNewEmployeeCommand => _AddNewEmployeeCommand
            ??= new LambdaCommand(OnAddNewEmployeeCommandExecuted, CanAddNewEmployeeCommandExecuted);

        private bool CanAddNewEmployeeCommandExecuted() => true;

        private void OnAddNewEmployeeCommandExecuted()
        {
            var new_employee = new Employee();

            if(!_UserDialog.Edit(new_employee)) return;

            _EmployeesCollection.Add(_EmployeeRepository.Add(new_employee));
            SelectedEmployee = new_employee;
        }

        #endregion

        #region Команда удаления указанного сотрудника

        private ICommand _RemoveEmployeeCommand;

        public ICommand RemoveEmployeeCommand => _RemoveEmployeeCommand
            ??= new LambdaCommand<Employee>(OnRemoveEmployeeCommandExecuted, CanRemoveEmployeeCommandExecuted);

        private bool CanRemoveEmployeeCommandExecuted(Employee e) => e != null || SelectedEmployee !=null;

        private void OnRemoveEmployeeCommandExecuted(Employee e)
        {
            var employee_to_remove = e ?? SelectedEmployee;
        }

        #endregion

        #region Команда загрузки данных из репозитория

        private ICommand _LoadDataCommand;

        public ICommand LoadDataCommand => _LoadDataCommand
            ??= new LambdaCommandAsync(OnLoadDataCommandExecuted, CanLoadDataCommandExecuted);

        private bool CanLoadDataCommandExecuted() => true;

        private async Task OnLoadDataCommandExecuted()
        {
            EmployeesCollection = new ObservableCollection<Employee>(await _EmployeeRepository.Items.ToArrayAsync());
        }

        #endregion


        public EmployeeViewModel()
        {
            if (!App.IsDesignTime)
                throw new InvalidOperationException("Данный конструктор не предназначен для использования вне дизайнера!");

            _ = OnLoadDataCommandExecuted();
        }

        public EmployeeViewModel(IRepository<Employee> EmployeeRepository, IUserDialog UserDialog)
        {
            _EmployeeRepository = EmployeeRepository;
            _UserDialog = UserDialog;
            _EmployeeViewSource = new CollectionViewSource
            {
                SortDescriptions =
                {
                    new SortDescription(nameof(Employee.Surname), ListSortDirection.Ascending)
                }
            };

            _EmployeeViewSource.Filter += OnEmployeeFilter;           
        }

        private void OnEmployeeFilter(object sender, FilterEventArgs e)
        {
            
            if (!(e.Item is Employee employee) || string.IsNullOrEmpty(EmployeeFilter)) return;

            if (!(employee.Name.ToLower().Contains(EmployeeFilter.ToLower()) || employee.Surname.ToLower().Contains(EmployeeFilter.ToLower()) || employee.Rank.ToLower().Contains(EmployeeFilter.ToLower())))
                e.Accepted = false;
        }
    }
}
