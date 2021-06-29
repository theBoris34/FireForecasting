using FireForecasting.DAL.Entityes.Departments;
using FireForecasting.Interfaces;
using MathCore.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Data;

namespace FireForecasting.ViewModels
{
    class FireEditorViewModel:ViewModel
    {
        #region Title - Заголовок окна
        private string _Title = "Редактирование пожаров";

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        #region TimeMessage - Время вызова
        private string _TimeMessage = "00:00";

        public string TimeMessage
        {
            get => _TimeMessage;
            set => Set(ref _TimeMessage, value);
        }
        #endregion

        #region Region - Район
        private string _Region;

        public string Region
        {
            get => _Region;
            set => Set(ref _Region, value);
        }
        #endregion  

        #region Address - Адрес
        private string _Address;

        public string Address
        {
            get => _Address;
            set => Set(ref _Address, value);
        }
        #endregion    

        #region RankFire - Ранг пожара
        private string _RankFire = "1";

        public string RankFire
        {
            get => _RankFire;
            set => Set(ref _RankFire, value);
        }
        #endregion  
        
        #region RanksFire - Список рангов пожара
        private List<string> _RanksFire;

        public List<string> RanksFire
        {
            get => _RanksFire;
            set => Set(ref _RanksFire, value);
        }
        #endregion

        #region DescriptionOfFire - Описание пожара
        private string _DescriptionOfFire;

        public string DescriptionOfFire
        {
            get => _DescriptionOfFire;
            set => Set(ref _DescriptionOfFire, value);
        }
        #endregion

        #region СauseOfFire - Причина пожара
        private string _СauseOfFire;

        public string СauseOfFire
        {
            get => _СauseOfFire;
            set => Set(ref _СauseOfFire, value);
        }
        #endregion

        #region CostOfDamage - Ущерб
        private decimal _CostOfDamage;

        public decimal CostOfDamage
        {
            get => _CostOfDamage;
            set => Set(ref _CostOfDamage, value);
        }
        #endregion

        #region CostOfSaved - Спасено
        private decimal _CostOfSaved;

        public decimal CostOfSaved
        {
            get => _CostOfSaved;
            set => Set(ref _CostOfSaved, value);
        }
        #endregion

        private string _DivisionFilter;

        public string DivisionFilter
        {
            get => _DivisionFilter;
            set
            {
                if (Set(ref _DivisionFilter, value))
                    _EmployeeViewSource.View.Refresh();
            }
        }

        private void OnDivisionFilter(object sender, FilterEventArgs e)
        {

            if (!(e.Item is Employee employee) || string.IsNullOrEmpty(DivisionFilter)) return;

            if (!(employee.Name.ToLower().Contains(DivisionFilter.ToLower()) || employee.Division.ToString().ToLower().Contains(DivisionFilter.ToLower())))
                e.Accepted = false;
        }

        private readonly IRepository<Employee> _EmployeeRepository;
        private readonly IRepository<Division> _DivisionRepository;


        //2. CollectionViewSource ему присвоить источник данных.
        //3. Свойство CollectionViewSource.View

        private readonly CollectionViewSource _EmployeeViewSource;

        public ICollectionView EmployeeView => _EmployeeViewSource.View;

        //public IEnumerable<Employee> Employees => _EmployeeRepository.Items;

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
                    OnPropertyChanged(nameof(EmployeeView));
                }
            }
        }

        public FireEditorViewModel()
        {

        }
        public FireEditorViewModel(IRepository<Employee> EmployeeRepository, IRepository<Division> DivisionRepository)
        {
            _EmployeeRepository = EmployeeRepository;
            _DivisionRepository = DivisionRepository; 
            _EmployeeViewSource = new CollectionViewSource
            {
                SortDescriptions =
                {
                    new SortDescription(nameof(Employee.Name), ListSortDirection.Ascending)
                }
            };
            EmployeesCollection = new ObservableCollection<Employee>(_EmployeeRepository.Items);

            RanksFire = new List<string>()                    //TODO: Вывести из модели-представления
            {                                                 //TODO: Вывести из модели-представления
                "1","1-Бис", "2", "3", "4", "5"               //TODO: Вывести из модели-представления
            };                                                //TODO: Вывести из модели-представления


            _EmployeeViewSource.Filter += OnDivisionFilter;


            //public Fire(DateTime date, string region, string adress, string rankOfFire, string DescriptionOfFire, string causeOfFire, decimal CostOfDamage, decimal CostOfSaved, Employee employee, Division division)
        }
    }
}
