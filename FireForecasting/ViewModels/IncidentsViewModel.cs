using FireForecasting.DAL.Entityes.Departments;
using FireForecasting.DAL.Entityes.Incidents;
using FireForecasting.Interfaces;
using FireForecasting.Models;
using FireForecasting.Views.Windows;
using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace FireForecasting.ViewModels
{
    class IncidentsViewModel : ViewModel
    {

        private readonly IRepository<Employee> _EmployeeRepository;
        private readonly IRepository<Division> _DivisionRepository;

        public string Title = "Список происшествий";


        private DateTime _SelectedDateBefore = DateTime.Today;
        public DateTime SelectedDateBefore
        {
            get => _SelectedDateBefore;
            set
            {
                if (Set(ref _SelectedDateBefore, value))
                    _IncidentViewSource.View.Refresh();
            }
        }

        private DateTime _SelectedDateAfter = DateTime.Today;
        public DateTime SelectedDateAfter
        {
            get => _SelectedDateAfter;
            set
            {
                if (Set(ref _SelectedDateAfter, value))
                    _IncidentViewSource.View.Refresh();
            }
        }


        private readonly IRepository<Fire> _FireRepository;

        public IEnumerable<Fire> Fires => _FireRepository.Items;

        //Фильтр
        //1. Свойство с текстом фильтра.
        //2. CollectionViewSource ему присвоить источник данных.
        //3. Свойство CollectionViewSource.View
        //4. Обработчик события фильтра
        //5. Соответсвие элемента фильтру, если нет выбрасываем.


        private string _IncidentFilter;
        public string IncidentFilter 
        {
            get => _IncidentFilter;
            set
            {
                if (Set(ref _IncidentFilter, value))
                    _IncidentViewSource.View.Refresh();
            }
        }

        private readonly CollectionViewSource _IncidentViewSource;
        public ICollectionView IncidentView => _IncidentViewSource.View;

        private void OnIncidentFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Fire incident)) return;
            if (!((incident.Date.Date >= SelectedDateBefore.Date) && (incident.Date.Date <= SelectedDateAfter.Date)))//если не содержит
                e.Accepted = false;

            if (string.IsNullOrEmpty(IncidentFilter)) return;

            if (!incident.Adress.ToLower().Contains(IncidentFilter.ToLower()))
                e.Accepted = false;
        }



        private ObservableCollection<Fire> _IncidentsCollection;
        public ObservableCollection<Fire> IncidentsCollection
        {
            get => _IncidentsCollection;
            set
            {
                if (Set(ref _IncidentsCollection, value))
                {
                    _IncidentViewSource.Source = value;
                    _IncidentViewSource.View.Refresh();
                    OnPropertyChanged(nameof(IncidentView));
                }
            }
        }


        #region Команда загрузки данных из репозитория

        private ICommand _LoadDataCommand;

        public ICommand LoadDataCommand => _LoadDataCommand
            ??= new LambdaCommandAsync(OnLoadDataCommandExecuted, CanLoadDataCommandExecuted);

        private bool CanLoadDataCommandExecuted() => true;

        private async Task OnLoadDataCommandExecuted()
        {
            IncidentsCollection = new ObservableCollection<Fire>(await _FireRepository.Items.ToArrayAsync());

        }

        #endregion


        #region Команда открытия окна редактирования пожаров
        private ICommand _EditFireCommand;

        public ICommand EditFireCommand => _EditFireCommand
            ??= new LambdaCommand(OnEditFireCommandExecuted, CanEditFireCommandExecuted);

        private bool CanEditFireCommandExecuted() => true;

        private void OnEditFireCommandExecuted()
        {
            var fire_editor_model = new FireEditorViewModel(_EmployeeRepository,_DivisionRepository);
            var fire_editor_window = new FireEditorWindow
            {
                DataContext = fire_editor_model
            };
            fire_editor_window.ShowDialog();
        }
        #endregion








        public IncidentsViewModel()
        {

        }


        public IQueryable FiresMonth { get; set; } 

        public IncidentsViewModel(IRepository<Employee> EmployeeRepository, IRepository<Division> DivisionRepository, IRepository<Fire> FireRepository)
        {
            _FireRepository = FireRepository;
            _EmployeeRepository = EmployeeRepository;
            _DivisionRepository = DivisionRepository;


            _IncidentViewSource = new CollectionViewSource
            {
                SortDescriptions =
                {
                    new SortDescription(nameof(Fire.Adress), ListSortDirection.Ascending)
                }
            };


            var FireStatics = new FireStatistic(FireRepository);
            

            _IncidentViewSource.Filter += OnIncidentFilter;

        }

        





    }

}
