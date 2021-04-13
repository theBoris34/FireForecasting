using FireForecasting.DAL.Entityes.Base;
using FireForecasting.DAL.Entityes.Departments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FireForecasting.DAL.Entityes.Incidents
{
    public class Fire : Entity
    {
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
        /// Дата пожара.
        /// </summary>
        public DateTime Date { get; set; }

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

        public Fire() { }

        public Fire(DateTime date, string region, string adress,string rankOfFire, string descriptionOfFire, string causeOfFire, decimal costOfDamage, decimal costOfSaved, Employee employee, Division division)
        {
            CostOfDamage = costOfDamage;
            CostOfSaved = costOfSaved;
            Date = date;
            Region = region;
            Adress = adress;
            RankOfFire = rankOfFire;
            DescriptionOfFire = descriptionOfFire;
            CauseOfFire = causeOfFire;
            Employee = employee;
            Division = division;
        }



    }
}
