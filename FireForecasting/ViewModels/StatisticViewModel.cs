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


        int[] Forecast_Mounth = new int[] { 79,  78, 69, 76, 77, 78, 72, 61, 63, 61, 75, 67, 67, 73, 78, 70, 80, 76, 80, 67, 69, 76, 66, 
                                            72, 79, 78, 76, 113, 83, 79, 93, 60, 50, 74, 58, 60, 71, 74, 70, 93, 71, 68, 63, 57, 60, 40 };
        int[] Forecast_Year = new int[] {  };
        int[] Forecast_Days = new int[] { 0, 4, 1, 1, 2, 3, 3, 7, 4, 2, 5, 1, 3, 3, 1, 2, 3, 0, 4, 4, 3, 2, 1, 1, 5, 4, 0, 0, 2, 1, 0, 1, 0, 4, 2, 1, 1, 4, 5, 2, 3, 6, 4, 2, 3, 3, 1, 2, 2, 1, 2, 5, 1, 6, 2, 2, 1, 2, 2, 1, 
            2, 1, 3, 1, 1, 3, 1, 0, 2, 1, 1, 3, 4, 2, 1, 3, 4, 3, 1, 1, 4, 2, 5, 1, 2, 1, 2, 1, 4, 3, 1, 4, 3, 0, 1, 4, 4, 2, 1, 2, 0, 2, 1, 1, 4, 5, 1, 3, 1, 6, 1, 1, 0, 2, 7, 6, 5, 2, 5, 3, 4, 1, 3, 3, 4, 4, 6, 4, 4, 2, 
            4, 1, 5, 1, 4, 2, 4, 2, 1, 2, 2, 1, 4, 2, 3, 0, 2, 3, 4, 3, 0, 1, 5, 2, 3, 2, 1, 2, 4, 6, 2, 2, 2, 0, 0, 1, 3, 2, 2, 3, 1, 3, 1, 1, 1, 2, 4, 5, 2, 2, 1, 5, 1, 0, 2, 2, 3, 1, 1, 2, 1, 2, 2, 4, 3, 2, 3, 1, 2, 0, 
            2, 3, 2, 3, 1, 5, 1, 0, 1, 2, 4, 2, 2, 0, 0, 1, 1, 3, 1, 3, 2, 1, 1, 3, 1, 2, 2, 2, 3, 2, 1, 1, 0, 0, 1, 0, 0, 1, 1, 1, 5, 3, 1, 1, 4, 4, 0, 0, 1, 1, 1, 5, 0, 2, 3, 3, 4, 2, 1, 2, 1, 0, 8, 4, 1, 2, 3, 1, 1, 1, 
            3, 3, 4, 3, 2, 2, 1, 3, 0, 3, 1, 0, 3, 1, 3, 1, 1, 2, 0, 0, 0, 1, 3, 6, 1, 2, 2, 3, 2, 2, 2, 3, 3, 1, 4, 0, 2, 3, 3, 3, 2, 6, 1, 2, 2, 1, 2, 2, 2, 3, 0, 2, 1, 2, 1, 1, 3, 4, 2, 2, 4, 0, 5, 2, 3, 1, 4, 2, 1, 0, 
            0, 0, 1, 2, 2, 3, 0, 4, 4, 2, 1, 3, 2, 1, 0, 4, 1, 1, 1, 1, 1, 1, 1, 2, 1, 3, 4, 0, 3, 1, 2, 1, 3, 2, 1, 1, 1, 1, 3, 3, 4, 4, 0, 3, 9, 3, 2, 2, 2, 1, 5, 2, 3, 1, 4, 0, 1, 1, 2, 2, 3, 2, 7, 1, 1, 0, 2, 4, 1, 4, 
            0, 1, 5, 2, 3, 1, 1, 5, 0, 3, 4, 2, 3, 1, 3, 2, 2, 2, 3, 2, 2, 1, 1, 3, 2, 2, 1, 1, 3, 2, 2, 2, 2, 0, 2, 6, 2, 3, 3, 2, 4, 2, 3, 3, 2, 4, 2, 0, 2, 1, 2, 1, 4, 6, 6, 2, 7, 4, 7, 2, 0, 2, 3, 0, 5, 9, 2, 1, 2, 1, 
            0, 3, 3, 4, 2, 1, 1, 3, 6, 0, 1, 0, 2, 4, 4, 0, 7, 3, 1, 2, 2, 2, 1, 2, 1, 1, 1, 4, 2, 1, 3, 4, 0, 5, 4, 5, 1, 1, 3, 1, 1, 5, 3, 2, 1, 4, 5, 2, 2, 4, 4, 1, 3, 5, 2, 0, 4, 1, 3, 2, 2, 3, 1, 5, 3, 6, 0, 3, 0, 3, 
            2, 1, 0, 3, 1, 0, 4, 0, 3, 1, 9, 2, 5, 1, 1, 1, 0, 0, 0, 2, 0, 1, 0, 2, 1, 3, 0, 5, 2, 3, 4, 1, 0, 4, 1, 2, 5, 1, 3, 2, 0, 2, 2, 1, 2, 2, 5, 1, 4, 2, 2, 2, 0, 2, 3, 4, 1, 2, 2, 2, 2, 3, 2, 2, 1, 4, 2, 1, 0, 4, 
            1, 2, 2, 3, 1, 2, 0, 4, 2, 4, 4, 0, 1, 3, 1, 2, 2, 2, 3, 3, 0, 0, 1, 4, 4, 1, 1, 3, 1, 0, 2, 3, 3, 2, 1, 4, 1, 2, 7, 6, 2, 1, 0, 2, 2, 0, 2, 4, 1, 2, 0, 1, 1, 5, 4, 3, 0, 1, 0, 5, 1, 4, 2, 4, 5, 4, 1, 4, 2, 2, 
            1, 2, 0, 4, 5, 2, 1, 0, 2, 9, 4, 3, 0, 4, 1, 2, 2, 6, 2, 2, 1, 2, 3, 5, 1, 1, 4, 1, 2, 3, 3, 4, 1, 2, 2, 0, 3, 4, 4, 3, 3, 4, 7, 2, 2, 3, 5, 2, 0, 3, 4, 5, 2, 3, 0, 4, 3, 5, 4, 5, 5, 3, 5, 3, 4, 5, 5, 3, 7, 1, 
            4, 2, 2, 5, 5, 1, 4, 1, 1, 1, 4, 3, 1, 3, 2, 2, 2, 3, 3, 4, 1, 1, 1, 1, 5, 5, 2, 1, 3, 2, 1, 5, 3, 1, 1, 0, 2, 2, 2, 3, 2, 4, 3, 2, 1, 3, 3, 5, 1, 3, 2, 4, 2, 7, 1, 1, 3, 3, 2, 5, 3, 2, 3, 2, 5, 5, 6, 4, 4, 3, 
            3, 3, 6, 3, 3, 5, 6, 4, 3, 7, 4, 4, 3, 6, 2, 4, 2, 3, 5, 0, 3, 1, 4, 3, 1, 4, 5, 4, 0, 4, 4, 1, 2, 1, 0, 1, 3, 5, 3, 0, 3, 1, 3, 3, 2, 0, 1, 2, 1, 3, 1, 4, 2, 0, 1, 0, 5, 4, 0, 2, 4, 0, 1, 2, 6, 2, 1, 3, 2, 2, 
            3, 4, 4, 4, 4, 6, 0, 1, 2, 2, 1, 1, 1, 1, 1, 0, 0, 0, 3, 1, 0, 0, 0, 2, 2, 2, 3, 2, 4, 2, 1, 3, 2, 1, 1, 1, 0, 1, 3, 1, 2, 2, 2, 2, 1, 3, 1, 1, 1, 3, 4, 1, 3, 1, 1, 2, 3, 2, 0, 0, 2, 2, 0, 2, 3, 2, 3, 6, 2, 4, 
            1, 2, 2, 1, 2, 3, 2, 2, 1, 1, 1, 3, 2, 2, 1, 3, 0, 3, 4, 2, 2, 1, 1, 4, 1, 2, 1, 1, 3, 3, 2, 4, 0, 2, 2, 2, 2, 1, 5, 2, 1, 1, 1, 1, 5, 1, 1, 2, 1, 1, 1, 1, 3, 1, 2, 1, 2, 2, 1, 3, 2, 0, 1, 1, 4, 3, 0, 1, 0, 0, 
            2, 1, 2, 0, 0, 0, 1, 2, 0, 0, 2, 4, 5, 1, 2, 2, 8, 3, 3, 3, 0, 5, 2, 2, 4, 4, 0, 1, 4, 2, 1, 5, 3, 2, 1, 3, 2, 3, 3, 2, 1, 0, 1, 4, 3, 2, 1, 1, 5, 2, 0, 0, 4, 1, 1, 1, 3, 2, 3, 4, 5, 1, 0, 1, 1, 1, 3, 2, 2, 3, 
            0, 3, 0, 6, 2, 4, 2, 2, 1, 4, 4, 2, 3, 0, 2, 2, 4, 2, 2, 3, 1, 4, 2, 3, 2, 6, 2, 2, 2, 2, 2, 1, 0, 2, 0, 1, 4, 2, 2, 3, 2, 3, 1, 0, 4, 6, 2, 0, 0, 2, 3, 2, 6, 0, 1, 2, 0, 5, 2, 2, 2, 3, 3, 1, 3, 1, 6, 5, 7, 7, 
            8, 4, 5, 6, 2, 3, 4, 1, 3, 3, 3, 2, 1, 2, 1, 9, 4, 0, 14, 1, 4, 2, 4, 5, 4, 2, 4, 7, 6, 7, 3, 0, 2, 1, 0, 8, 4, 2, 4, 4, 0, 3, 5, 0, 3, 1, 4, 1, 1, 3, 0, 2, 3, 4, 2, 6, 3, 4, 3, 2, 4, 3, 2, 1, 4, 1, 2, 1, 1, 1, 
            2, 2, 2, 4, 0, 0, 3, 2, 2, 6, 1, 4, 2, 1, 3, 8, 4, 0, 1, 4, 0, 1, 2, 2, 2, 1, 2, 2, 3, 1, 1, 2, 3, 1, 2, 2, 1, 2, 2, 0, 2, 1, 1, 1, 3, 4, 6, 1, 2, 0, 3, 2, 3, 1, 4, 0, 2, 1, 0, 2, 0, 2, 2, 0, 5, 4, 1, 1, 2, 0, 
            2, 1, 4, 2, 1, 3, 0, 3, 2, 5, 1, 0, 3, 1, 1, 0, 3, 2, 3, 1, 0, 3, 1, 6, 4, 4, 4, 1, 0, 1, 3, 5, 5, 1, 2, 4, 2, 1, 0, 2, 3, 4, 2, 2, 2, 2, 1, 1, 3, 0, 5, 0, 5, 2, 1, 0, 1, 3, 2, 2, 3, 2, 3, 1, 1, 3, 2, 1 };

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

            //2 график 
            FunctionSeries function_2 = new FunctionSeries();
            function_2.TrackerFormatString = "Прогноз пожаров: {4:0.###} Дата: {2:MM-yyyy}";

            int i = 0;
            foreach (var M in _fireStatistic.FiresPerMounth)
            {
                function_2.Points.Add(new DataPoint(DateTimeAxis.ToDouble(M.Key), (Forecast_Mounth[i])));
                i++;
            }
            //2 график

            double Errors;
            double SumErrors = 0;
            i = 0;
            foreach (var M in _fireStatistic.FiresPerMounth.Values)
            {
                Errors = Math.Abs(M - Forecast_Mounth[i]) / M;
                SumErrors += Errors;
                i++;
                if (i == Forecast_Mounth.Length) break;
            }
            ForecastAccuracy = Math.Round(SumErrors / _fireStatistic.FiresPerMounth.Count * 100, 2);

            this.MyModel.Series.Add(function);
            this.MyModel.Series.Add(function_2);
            this.MyModel.Axes.Add(xAxis);
            this.MyModel.Axes.Add(new LinearAxis());
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

            //2 график 
            FunctionSeries function_2 = new FunctionSeries();
            function_2.TrackerFormatString = "Прогноз пожаров: {4:0.###} Дата: {2:dd-MM-yyyy}";

            int i = 0;
            foreach (var M in _fireStatistic.FiresPerDay)
            {
                function_2.Points.Add(new DataPoint(DateTimeAxis.ToDouble(M.Key), (Forecast_Days[i])));
                i++;
            }
            //2 график

            double Errors;
            double SumErrors = 0;
            i = 0;
            foreach (var M in _fireStatistic.FiresPerDay.Values)
            {
                Errors = Math.Abs(M - Forecast_Days[i]) / M;
                SumErrors += Errors;
                i++;
                if (i == Forecast_Days.Length) break;
            }
            ForecastAccuracy = Math.Round(SumErrors / _fireStatistic.FiresPerDay.Count * 100, 2);

            this.MyModel.Series.Add(function);
            this.MyModel.Series.Add(function_2);
            this.MyModel.Axes.Add(xAxis);
            this.MyModel.Axes.Add(new LinearAxis());
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
