using FireForecasting.DAL.Entityes.Departments;
using FireForecasting.DAL.Entityes.Incidents;
using FireForecasting.Interfaces;
using FireForecasting.Models;
using FireForecasting.Services.Intarface;
using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using Microsoft.EntityFrameworkCore;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FireForecasting.ViewModels
{
    class StatisticViewModel : ViewModel
    {
        private readonly IUserDialog _UserDialug;
        private readonly IRepository<Employee> _EmployeeRepository;
        private readonly IRepository<Division> _DivisionRepository;
        private readonly IRepository<Fire> _FireRepository;
        private readonly FireStatistic _fireStatistic;
        private double _ForecastAccuracy;

        /// <summary>
        /// Точность прогноза.
        /// </summary>
        public double ForecastAccuracy 
        {   get => _ForecastAccuracy;
            set
            {
                if (Set(ref _ForecastAccuracy, value))
                {                  
                    OnPropertyChanged(nameof(ForecastAccuracy));
                }
            }
        }

        public PlotModel MyModel { get; private set; }

        public ObservableCollection<ManyFiresDivisionInfo> ManyFiresDivision { get; } = new ObservableCollection<ManyFiresDivisionInfo>();

        #region Команда вычисления статистики

        private ICommand _ComputeStatisticCommand;

        public ICommand ComputeStatisticCommand => _ComputeStatisticCommand
            ??= new LambdaCommandAsync(OnComputeStatisticCommandExecuted);

        /// <summary> Логика вычисления подразделения с наибольшим количеством пожаров. </summary>
        private async Task OnComputeStatisticCommandExecuted()
        {
            await ComputeManyFiresDivisionAsync();
        }

        private async Task ComputeManyFiresDivisionAsync()
        {
            var manyFiresDivision_query = _FireRepository.Items
                .GroupBy(f => f.Division.Id)
                .Select(fires => new { DivisionId = fires.Key, Count = fires.Count()})
                .OrderByDescending(fires => fires.Count)
                .Take(10)
                .Join(_DivisionRepository.Items, 
                    f => f.DivisionId, 
                    d => d.Id, 
                    (f, d) => new ManyFiresDivisionInfo { Division = d, FiresCount = f.Count});


            ManyFiresDivision.Clear();
            foreach (var manyFiresDivision in await manyFiresDivision_query.ToArrayAsync())
                ManyFiresDivision.Add(manyFiresDivision);
        }

        #endregion

        #region Команда отображения графика прогнозной модели на месяца

        private ICommand _PlotFiresOfMounthCommand;

        public ICommand PlotFiresOfMounthCommand => _PlotFiresOfMounthCommand
            ??= new LambdaCommand(OnPlotFiresOfMounthCommandExecuted, CanPlotFiresOfMounthCommandExecuted);

        private bool CanPlotFiresOfMounthCommandExecuted() => true;

        private void OnPlotFiresOfMounthCommandExecuted()
        {
            this.MyModel.InvalidatePlot(true);
            this.MyModel.Series.Clear();
            this.MyModel.Axes.Clear();

            var minValue = DateTimeAxis.ToDouble(_fireStatistic.FiresPerMounth.Keys.First());
            var maxValue = DateTimeAxis.ToDouble(_fireStatistic.FiresPerMounth.Keys.Last());

            DateTimeAxis xAxis = new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                StringFormat = "MM.yyyy",
                Title = "Месяц",
                MinorIntervalType = DateTimeIntervalType.Days,
                IntervalType = DateTimeIntervalType.Days,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.None
            };

            FunctionSeries function = new FunctionSeries();
            function.TrackerFormatString = "Пожаров: {4:0.###} Дата: {2:MM-yyyy}";

            foreach (var M in _fireStatistic.FiresPerMounth)
            {
                function.Points.Add(new DataPoint(DateTimeAxis.ToDouble(M.Key), M.Value));
            }

            ////2 график 
            //FunctionSeries function_2 = new FunctionSeries();
            //function_2.TrackerFormatString = "Прогноз пожаров: {4:0.###} Дата: {2:MM-yyyy}";

            //int i = 0;
            //foreach (var M in _fireStatistic.FiresPerMounth)
            //{
            //    function_2.Points.Add(new DataPoint(DateTimeAxis.ToDouble(M.Key), (Forecast_Mounth[i])));
            //    i++;
            //}
            ////2 график

            //double Errors;
            //double SumErrors = 0;
            //i = 0;
            //foreach (var M in _fireStatistic.FiresPerMounth.Values)
            //{
            //    Errors = Math.Abs(M - Forecast_Mounth[i]) / M;
            //    SumErrors += Errors;
            //    i++;
            //    if (i == Forecast_Mounth.Length) break;
            //}
            //ForecastAccuracy = Math.Round(SumErrors / _fireStatistic.FiresPerMounth.Count * 100, 2);

            this.MyModel.Series.Add(function);
            this.MyModel.Axes.Add(xAxis);
            this.MyModel.InvalidatePlot(true); 
        }


        #endregion

        #region Команда отображения графика прогнозной модели на день

        private ICommand _PlotFiresOfDaysCommand;

        public ICommand PlotFiresOfDaysCommand => _PlotFiresOfDaysCommand
            ??= new LambdaCommand(OnPlotFiresOfDaysCommandExecuted, CanPlotFiresOfDaysCommandExecuted);

        private bool CanPlotFiresOfDaysCommandExecuted() => true;

        private void OnPlotFiresOfDaysCommandExecuted()
        {
            this.MyModel.InvalidatePlot(true);
            this.MyModel.Series.Clear();
            this.MyModel.Axes.Clear();

            var minValue = DateTimeAxis.ToDouble(_fireStatistic.FiresPerDay.Keys.First());
            var maxValue = DateTimeAxis.ToDouble(_fireStatistic.FiresPerDay.Keys.Last());

            DateTimeAxis xAxis = new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                StringFormat = "dd.MM.yyyy",
                Title = "День",
                MinorIntervalType = DateTimeIntervalType.Days,
                IntervalType = DateTimeIntervalType.Days,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.None,
            };

            FunctionSeries function = new FunctionSeries();
            function.TrackerFormatString = "Пожаров: {4:0.###} Дата: {2:dd-MM-yyyy}";
            foreach (var M in _fireStatistic.FiresPerDay)
            {
                function.Points.Add(new DataPoint(DateTimeAxis.ToDouble(M.Key), M.Value));
            }
           

            this.MyModel.Series.Add(function);
            this.MyModel.Axes.Add(xAxis);
            this.MyModel.InvalidatePlot(true);
        }

        #endregion

        public StatisticViewModel(IRepository<Employee> employeeRepository, IRepository<Division> divisionRepository, IRepository<Fire> fireRepository, IUserDialog UserDialog)
        {
            _UserDialug = UserDialog;
            _EmployeeRepository = employeeRepository;
            _DivisionRepository = divisionRepository;
            _FireRepository = fireRepository;
            _fireStatistic = new FireStatistic(fireRepository);
            this.MyModel = new PlotModel { Title = "Динамика пожарной обстановки в регионе" };
            //PlotForecastingMounth();
            //PlotForecastingDays();

        }

        void PlotForecastingDays()
        {
           
        }

        void PlotForecastingMounth()
        {
            
        }
    }
}
