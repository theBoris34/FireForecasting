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
        private readonly IRepository<Division> _DivisionRepository;
        private readonly IUserDialog _UserDialog;


        private ObservableCollection<Division> _DivisionsCollection;
        public ObservableCollection<Division> DivisionsCollection
        {
            get => _DivisionsCollection;
            set
            {
                if (Set(ref _DivisionsCollection, value))
                {
                    _DivisionViewSource.Source = value;
                    _DivisionViewSource.View.Refresh();
                    OnPropertyChanged(nameof(DivisionView));
                }
            }
        }

        private readonly CollectionViewSource _DivisionViewSource;

        public ICollectionView DivisionView => _DivisionViewSource.View;


        public IEnumerable<Division> Divisions => _DivisionRepository.Items;

        /// <summary> Выбранный сотрудник </summary>
        private Division _SelectedDivision;

        public Division SelectedDivision
        {
            get => _SelectedDivision;
            set => Set(ref _SelectedDivision, value);
        }

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

        #region EmployeeFilter - Искомое слово
        
        //Фильтр
        //1. Свойство с текстом фильтра.
        //2. CollectionViewSource ему присвоить источник данных.
        //3. Свойство CollectionViewSource.View
        //4. Обработчик события фильтра
        //5. Соответсвие элемента фильтру, если нет выбрасываем.

        private string _DivisionFilter;

        public string DivisionFilter
        {
            get => _DivisionFilter;
            set
            {
                if (Set(ref _DivisionFilter, value))
                    _DivisionViewSource.View.Refresh();
            }
        }

        #endregion

        private readonly CollectionViewSource _EmployeeViewSource;

        public ICollectionView EmployeesView => _EmployeeViewSource.View;


        //public IEnumerable<Employee> SelectedDivisionEmployees => _EmployeeRepository.Items.Where(e => e.Division == SelectedDivision);

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

            if (!_UserDialog.ConfirmWarning($"Удалить сотрудника {employee_to_remove}?", "Удаление сотрудника")) 
                return;

            _EmployeeRepository.Remove(employee_to_remove.Id);
            EmployeesCollection.Remove(employee_to_remove);
            if(ReferenceEquals(SelectedEmployee, employee_to_remove))
                SelectedEmployee = null;
        }

        #endregion

        #region Команда загрузки данных из репозитория

        private ICommand _LoadDataCommand;

        public ICommand LoadDataCommand => _LoadDataCommand
            ??= new LambdaCommandAsync(OnLoadDataCommandExecuted, CanLoadDataCommandExecuted);

        private bool CanLoadDataCommandExecuted() => true;

        private async Task OnLoadDataCommandExecuted()
        {
            DivisionsCollection = new ObservableCollection<Division>(await _DivisionRepository.Items.ToArrayAsync());
            EmployeesCollection = new ObservableCollection<Employee>(await _EmployeeRepository.Items.ToArrayAsync());
        }

        #endregion

        

        public EmployeeViewModel()
        {
            if (!App.IsDesignTime)
                throw new InvalidOperationException("Данный конструктор не предназначен для использования вне дизайнера!");

            _ = OnLoadDataCommandExecuted();
        }

        public EmployeeViewModel(IRepository<Employee> EmployeeRepository, IRepository<Division> DivisionRepository, IUserDialog UserDialog)
        {
            _DivisionRepository = DivisionRepository;
            _EmployeeRepository = EmployeeRepository;
            _UserDialog = UserDialog;

            _DivisionViewSource = new CollectionViewSource
            {
                SortDescriptions =
                {
                    new SortDescription(nameof(Division.Name), ListSortDirection.Ascending)                
                }
            };

            _EmployeeViewSource = new CollectionViewSource
            {
                SortDescriptions =
                {
                    new SortDescription(nameof(Employee.Surname), ListSortDirection.Ascending)
                }
            };

            _DivisionViewSource.Filter += OnDivisionFilter;
            _EmployeeViewSource.Filter += OnEmployeeFilter;
        }

        private void OnEmployeeFilter(object sender, FilterEventArgs e)
        {
            
            if (!(e.Item is Employee employee) || string.IsNullOrEmpty(EmployeeFilter)) return;

            if (!(employee.Name.ToLower().Contains(EmployeeFilter.ToLower()) || employee.Surname.ToLower().Contains(EmployeeFilter.ToLower()) || employee.Rank.ToLower().Contains(EmployeeFilter.ToLower())))
                e.Accepted = false;
        }

        private void OnDivisionFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Division division) || string.IsNullOrEmpty(DivisionFilter)) return;

            if (!division.Name.ToLower().Contains(DivisionFilter.ToLower()))
                e.Accepted = false;
        }
    }
}
