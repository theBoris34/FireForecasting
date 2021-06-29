using FireForecasting.DAL.Entityes.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireForecasting.DAL.Entityes.Departments
{
    public class FireTruck:FireTruckBase
    {
        /// <summary>
        /// Подразделение дислокации автомобиля.
        /// </summary>
        public virtual Division Division { get; set; }

        /// <summary>
        /// Экипаж пожарного автомобиля.
        /// </summary>
        public virtual List<Employee> Crew { get; set; }

        public FireTruck()
        {

        }
        public FireTruck(FireTruckBase fireTruckBase, Division division)
            :base(fireTruckBase.Brand, fireTruckBase.Model, fireTruckBase.YearOfCreation, fireTruckBase.NumberOfSeats, fireTruckBase.MaxSpeed, fireTruckBase.FuelVolume)
        {
            Division = division ?? throw new ArgumentNullException(nameof(division));
        }

    }
}
