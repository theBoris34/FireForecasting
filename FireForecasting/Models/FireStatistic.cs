using FireForecasting.DAL.Entityes.Incidents;
using FireForecasting.Interfaces;
using System;
using System.Collections.Generic;
using MathCore.WPF.ViewModels;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using OxyPlot;
using OxyPlot.Series;

namespace FireForecasting.Models
{
    class FireStatistic : ViewModel
    {
        private readonly IRepository<Fire> _FireRepository;

        public IQueryable FiresMonth { get; set; }

        private Dictionary<DateTime, int> _FiresPerDay;

        public Dictionary<DateTime, int> FiresPerDay
        {
            get
            {
                return _FireRepository.Items.GroupBy(x => new { Date = x.Date.Date })
                    .Select(x => new
                    {
                        FiresCount = x.Count(),
                        Date = x.Key.Date
                    }).OrderBy(x => x.Date)
                    .ToDictionary(f => f.Date, f => f.FiresCount);
            }
            set => Set(ref _FiresPerDay, value);
        }

        private Dictionary<DateTime, int> _FiresPerMounth;
        public Dictionary<DateTime, int> FiresPerMounth
        {
            get
            {
                return _FireRepository.Items.GroupBy(x => new { Date = x.Date.Date.Month, Year = x.Date.Date.Year })
                    .Select(x => new
                    {
                        FiresCount = x.Count(),
                        Month = x.Key.Date,
                        Year = x.Key.Year
                    }).OrderBy(x => x.Year).ThenBy(x => x.Month)
                    .ToDictionary(f => new DateTime(f.Year,f.Month,1), f => f.FiresCount);
            }
            set => Set(ref _FiresPerMounth, value);
        }

        private Dictionary<DateTime, int> _FiresPerYear;
        public Dictionary<DateTime, int> FiresPerYear
        {
            get
            {
                return _FireRepository.Items.GroupBy(x => new { Year = x.Date.Date.Year })
                    .Select(x => new
                    {
                        FiresCount = x.Count(),
                        Year = x.Key.Year
                    }).OrderBy(x => x.Year)
                    .ToDictionary(f => new DateTime(f.Year, 1, 1), f => f.FiresCount);
            }
            set => Set(ref _FiresPerYear, value);
        }

        public FireStatistic(IRepository<Fire> fireRepository)
        {
            _FireRepository = fireRepository;
            GetFirePerMonth();


        }

        public void GetFirePerMonth()
        {

            ///Группировка по месяцам (количество пожаров за месяц)
            





            //TODO: по неделям

        }
    }
}
