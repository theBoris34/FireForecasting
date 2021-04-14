using FireForecasting.DAL.Entityes.Incidents;
using FireForecasting.Interfaces;
using MathCore.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireForecasting.ViewModels
{
    class IncidentsViewModel : ViewModel
    {

        public string Title = "Список происшествий";
        public DateTime SelectedDate = DateTime.Today;

        private readonly IRepository<Fire> _FireRepository;

        public IEnumerable<Fire> Fires => _FireRepository.Items;

        public IncidentsViewModel()
        {

        }

        public IncidentsViewModel(IRepository<Fire> FireRepository)
        {
            _FireRepository = FireRepository;
        }
    }
}
