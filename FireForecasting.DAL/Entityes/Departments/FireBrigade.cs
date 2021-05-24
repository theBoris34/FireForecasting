using FireForecasting.DAL.Entityes.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireForecasting.DAL.Entityes.Departments
{


    /// <summary>
    /// Отделение (автомобиль + сотрудники).
    /// </summary>
    public class FireBrigade:Entity
    {
        /// <summary>
        /// Автомобиль.
        /// </summary>
        public FireTruck FireTruck { get; set; }
        /// <summary>
        /// Список личного состава.
        /// </summary>
        public List<Employee> Crew { get; set; }
        public FireBrigade()
        {

        }
        public FireBrigade(FireTruck fireTruck, List<Employee> crew)
        {
            FireTruck = fireTruck;
            Crew = crew;
        }

    }
}
