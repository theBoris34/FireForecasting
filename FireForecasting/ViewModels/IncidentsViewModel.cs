using FireForecasting.DAL.Entityes.Incidents;
using FireForecasting.Interfaces;
using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace FireForecasting.ViewModels
{
    class IncidentsViewModel : ViewModel
    {

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











        public IncidentsViewModel()
        {

        }

        public IncidentsViewModel(IRepository<Fire> FireRepository)
        {
            _FireRepository = FireRepository;

            _IncidentViewSource = new CollectionViewSource
            {
                SortDescriptions =
                {
                    new SortDescription(nameof(Fire.Adress), ListSortDirection.Ascending)
                }
            };

            _IncidentViewSource.Filter += OnIncidentFilter;

           
        }





    }
}
