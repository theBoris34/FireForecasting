using FireForecasting.DAL.Entityes.Departments;
using MathCore.WPF.ViewModels;
using System;

namespace FireForecasting.ViewModels
{
    internal class EmployeeEditorViewModel:ViewModel
    {
        #region Title - Заголовок окна
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

        #region IdEmployee - Идентификатор сотрудника

        public int IdEmployee { get; }
        #endregion

        public EmployeeEditorViewModel()
            :this(new Employee { Surname = "Иванов", Name = "Иван", Patronymic = "Иванович", Rank = "рядовой вн.службы"})
        {
            if (!App.IsDesignTime)
                throw new InvalidOperationException("Конструктор не предназначен для работы вне дизайнера!");
        }

        public EmployeeEditorViewModel(Employee employee)
        {
            IdEmployee = employee.Id;
            Name = employee.Name;
            Surname = employee.Surname;
            Patronymic = employee.Patronymic;
            Rank = employee.Rank;

        }

    }
}
