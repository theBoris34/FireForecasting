using FireForecasting.DAL.Entityes.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireForecasting.DAL.Entityes.Departments
{
    /// <summary>
    /// Пожарный автомобиль
    /// </summary>
    class FireTruck:Entity
    {
      
        /// <summary>
        /// Тип ПА (АЦ,АЛ и тп.)
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Производитель автомобиля.
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Модель автомобиля.
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Год выпуска авто.
        /// </summary>
        public DateTime YearOfCreation { get; set; }

        /// <summary>
        /// Состояние авто - в строю, ремонт, списан.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Модель пожарной установки.
        /// </summary>
        public string FireEngine { get; set; }

        /// <summary>
        /// Количество мест в автомобиле - шт.
        /// </summary>
        public byte NumberOfSeats { get; set; }

        /// <summary>
        /// Максимальная скорость - км/ч.
        /// </summary>
        public byte MaxSpeed { get; set; }

        /// <summary>
        /// Объем цистерны для воды - м3.
        /// </summary>
        public int TankVolume { get; set; }

        /// <summary>
        /// Объем цистерны для пенообразователя - м3.
        /// </summary>
        public int FoamVolume { get; set; }

        /// <summary>
        /// Объем бензобака - м3.
        /// </summary>
        public int FuelVolume { get; set; }


        /// <summary>
        /// Производительность пожарного насоса – л/сек.
        /// </summary>
        public double PumpCapacity { get; set; }


        /// <summary>
        /// Высота подъема стрелы - м.
        /// </summary>
        public int LiftingHeight { get; set; }

        public override string ToString()
        {
            return Type + Brand + "-" + Model;
        }

    }
}
