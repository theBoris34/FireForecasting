using FireForecasting.DAL.Entityes.Base;
using FireForecasting.DAL.Entityes.Departments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FireForecasting.DAL.Entityes.Incidents
{
    public class Fire : Entity
    {

        #region Свойства
        /// <summary>
        /// Сумма ущерба.
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal CostOfDamage { get; set; }

        /// <summary>
        /// Сумма спасёного.
        /// </summary>
        public decimal CostOfSaved { get; set; }

        /// <summary>
        /// Район пожара.
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Адрес пожара.
        /// </summary>
        public string Adress { get; set; }

        /// <summary>
        /// Расстояние до пожара.
        /// </summary>
        public double DistanceToFire { get; set; }

        /// <summary>
        /// Дата пожара.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Время выезда.
        /// </summary>
        public DateTime CheckOutTime { get; set; }

        /// <summary>
        /// Время прибытия.
        /// </summary>
        public DateTime ArrivalTime { get; set; }

        /// <summary>
        /// Время подачи первого ствола.
        /// </summary>
        public DateTime FirstBarrelTime { get; set; }

        /// <summary>
        /// Время локализации.
        /// </summary>
        public DateTime LocalizationTime { get; set; }

        /// <summary>
        /// Время ликвидации.
        /// </summary>
        public DateTime LiquidationTime { get; set; }

        /// <summary>
        /// Время полной ликвидации и окончании работ.
        /// </summary>
        public DateTime CompletionTime { get; set; }

        /// <summary>
        /// Продолжительность локализации.
        /// </summary>
        public DateTime  DurationOfLocalization { get; set; }

        /// <summary>
        /// Продолжительность ликвидации.
        /// </summary>
        public DateTime DurationOfLiquidation { get; set; }

        /// <summary>
        /// Продолжительность работ.
        /// </summary>
        public DateTime DurationOfWork { get; set; }

        /// <summary>
        /// Причина пожара.
        /// </summary>
        public string CauseOfFire { get; set; }

        /// <summary>
        /// Ранг пожара.
        /// </summary>
        public string RankOfFire { get; set; }

        /// <summary>
        /// Описание пожара.
        /// </summary>
        public string DescriptionOfFire { get; set; }

        /// <summary> 
        /// Сотрудники учавствующие в тушении пожара. 
        /// </summary>
        public virtual Employee Employee {get;set;}

        /// <summary> 
        /// Подразделения учавствующие в тушении пожара.
        /// </summary>
        public virtual Division Division { get; set; }

        /// <summary>
        /// Количество автоцистерн.
        /// </summary>
        public int CountOfCistern { get; }

        /// <summary>
        /// Количество автолестниц.
        /// </summary>
        public int CountOfStairs { get; }

        /// <summary>
        /// Погибло.
        /// </summary>
        public int Casualties { get; set; }        

        /// <summary>
        /// Пострадало.
        /// </summary>
        public int Affected { get; set; }



        #endregion

        public Fire() { }

        public Fire(DateTime date, DateTime checkOutTime, DateTime arrivalTime, DateTime firstBarrelTime, DateTime localizationTime,                    
                    DateTime liquidationTime, DateTime completionTime, string region, string adress,double distanceToFire, string rankOfFire, int casualties, int affected, string descriptionOfFire, 
                    string causeOfFire, decimal costOfDamage, decimal costOfSaved, Employee employee, Division division, int countOfCistern, int countOfStairs)
        {
            CostOfDamage = costOfDamage;
            CostOfSaved = costOfSaved;
            Date = date;
            CheckOutTime = checkOutTime;
            ArrivalTime = arrivalTime;
            FirstBarrelTime = firstBarrelTime;
            LocalizationTime = localizationTime;
            LiquidationTime = liquidationTime;
            CompletionTime = completionTime;
            Region = region;
            Adress = adress;
            DistanceToFire = distanceToFire;
            RankOfFire = rankOfFire;
            Casualties = casualties;
            Affected = affected;
            DescriptionOfFire = descriptionOfFire;
            CauseOfFire = causeOfFire;
            Employee = employee;
            Division = division;
            CountOfCistern = countOfCistern;
            CountOfStairs = countOfStairs;
        }



    }
}
