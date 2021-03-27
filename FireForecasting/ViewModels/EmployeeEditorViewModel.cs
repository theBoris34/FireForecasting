using FireForecasting.DAL.Entityes.Departments;
using FireForecasting.Interfaces;
using FireForecasting.Services.Intarface;
using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace FireForecasting.ViewModels
{
    internal class EmployeeEditorViewModel:ViewModel
    {
        public Employee SelectedEmployee { get; }
        private readonly IUserDialog _UserDialog;
        public readonly IRepository<Division> _DivisionRepository;

        #region Divisions - Список подразделений.
        private IQueryable _Divisions;

        public IQueryable Divisions
        {
            get => _DivisionRepository.Items;
            set => Set(ref _Divisions, value);
        }
        #endregion

        #region Title - Заголовок окна.
        private string _Title = "Редактирование сотрудника";

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        #region Name - Имя сотрудника
        private string _Name;

        public string Name
        {
            get => _Name;
            set => Set(ref _Name, value);
        }
        #endregion

        #region Surname - Фамилия сотрудника
        private string _Surname;

        public string Surname
        {
            get => _Surname;
            set => Set(ref _Surname, value);
        }
        #endregion

        #region Patronymic - Отчество сотрудника
        private string _Patronymic;

        public string Patronymic
        {
            get => _Patronymic;
            set => Set(ref _Patronymic, value);
        }
        #endregion

        #region Rank - Звание сотрудника
        private string _Rank;

        public string Rank
        {
            get => _Rank;
            set => Set(ref _Rank, value);
        }
        #endregion

        #region Division - Подразделение сотрудника
        private Division _Division;

        public Division Division
        {
            get => _Division;
            set => Set(ref _Division, value);
        }
        #endregion

        #region IdEmployee - Идентификатор сотрудника

        public int IdEmployee { get; }
        #endregion


        public EmployeeEditorViewModel()
            //:this(new Employee { Surname = "Иванов", Name = "Иван", Patronymic = "Иванович", Rank = "рядовой вн.службы"})
        {
            if (!App.IsDesignTime)
                throw new InvalidOperationException("Конструктор не предназначен для работы вне дизайнера!");
        }

        public EmployeeEditorViewModel(Employee employee, IRepository<Division> DivisionRepository)
        {

            SelectedEmployee = employee;
            _DivisionRepository = DivisionRepository;
            IdEmployee = employee.Id;
            Name = employee.Name;
            Surname = employee.Surname;
            Patronymic = employee.Patronymic;
            Rank = employee.Rank;
            Division = employee.Division;
            
        }

        #region Команда удаления указанного сотрудника

        private ICommand _EditEmployeeCommand;

        public ICommand EditEmployeeCommand => _EditEmployeeCommand
            ??= new LambdaCommand<Employee>(OnEditEmployeeCommandExecuted, CanEditEmployeeCommandExecuted);

        private bool CanEditEmployeeCommandExecuted(Employee e) => e != null || SelectedEmployee != null;

        private void OnEditEmployeeCommandExecuted(Employee e)
        {
            var employee_to_remove = e ?? SelectedEmployee;

            if (!_UserDialog.ConfirmWarning($"Удалить сотрудника {employee_to_remove}?", "Удаление сотрудника"))
                return;

            //_EmployeeRepository.Edit(employee_to_remove.Id);
            //EmployeesCollection.Edit(employee_to_remove);
            //if (ReferenceEquals(SelectedEmployee, employee_to_remove))
            //    SelectedEmployee = null;
        }
        #endregion

    }
}
