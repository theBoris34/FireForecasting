using FireForecasting.DAL.Entityes.Departments;
using FireForecasting.Interfaces;
using MathCore.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace FireForecasting.ViewModels
{
    class EmployeeViewModel : ViewModel
    {
        private readonly IRepository<Employee> _EmployeeRepository;

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

        public EmployeeViewModel()
        {
            if (!App.IsDesignTime)
                throw new InvalidOperationException("Данный конструктор не предназначен для использования вне дизайнера!");
        }

        public EmployeeViewModel(IRepository<Employee> EmployeeRepository)
        {
            _EmployeeRepository = EmployeeRepository;

            _EmployeeViewSource = new CollectionViewSource
            {
                Source = _EmployeeRepository.Items.ToArray(),
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
