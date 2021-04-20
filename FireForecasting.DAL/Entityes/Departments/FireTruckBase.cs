using FireForecasting.DAL.Entityes.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireForecasting.DAL.Entityes.Departments
{
    /// <summary>
    /// Пожарный автомобиль
    /// </summary>
    class FireTruckBase:Entity
    {
        #region Свойства
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
        #endregion

        public FireTruckBase() : this("None", "None",DateTime.Now,1,60,50)
        {

        }

        /// <summary>
        /// Создание экземпляра Пожарного автомобиля.
        /// </summary>
        /// <param name="brand">Производитель автомобиля.</param>
        /// <param name="model">Модель автомобиля.</param>
        /// <param name="yearOfCreation">Год выпуска.</param>
        /// <param name="numberOfSeats">Количество мест.</param>
        /// <param name="maxSpeed">Максимальная скорость.</param>
        /// <param name="fuelVolume">Объем бензобака.</param>
        public FireTruckBase(string brand, string model, DateTime yearOfCreation, byte numberOfSeats, byte maxSpeed, int fuelVolume)
        {         

            if (string.IsNullOrEmpty(brand))
            {
                throw new ArgumentException("Производитель ПА не может быть пустым или иметь значение null", nameof(brand));
            }

            if (string.IsNullOrEmpty(model))
            {
                throw new ArgumentException("Модель ПА не может быть пустым или иметь значение null", nameof(model));
            }

            Brand = brand;
            Model = model;
            YearOfCreation = yearOfCreation;
            NumberOfSeats = numberOfSeats;
            MaxSpeed = maxSpeed;
            FuelVolume = fuelVolume;
        }

        /// <summary>
        /// Создание экземпляра Пожарного автомобиля с цистерной.
        /// </summary>
        /// <param name="brand">Производитель автомобиля.</param>
        /// <param name="model">Модель автомобиля.</param>
        /// <param name="yearOfCreation">Год выпуска.</param>
        /// <param name="numberOfSeats">Количество мест.</param>
        /// <param name="maxSpeed">Максимальная скорость.</param>
        /// <param name="fuelVolume">Объем бензобака.</param>
        /// <param name="fireEngine">Модель пожарной установки.</param>
        /// <param name="tankVolume">Объем цистерны.</param>
        /// <param name="foamVolume">Объем пенообразователя.</param>
        /// <param name="pumpCapacity">Производительность насоса.</param>
        public FireTruckBase(string type, string brand, string model, DateTime yearOfCreation, byte numberOfSeats, byte maxSpeed, int fuelVolume, string fireEngine, int tankVolume, int foamVolume, double pumpCapacity) 
            : this(brand, model, yearOfCreation, numberOfSeats, maxSpeed, fuelVolume)
        {
            if (string.IsNullOrEmpty(type))
            {
                throw new ArgumentException("Тип ПА не может быть пустым или иметь значение null", nameof(type));
            }
            FireEngine = fireEngine;
            TankVolume = tankVolume;
            FoamVolume = foamVolume;
            PumpCapacity = pumpCapacity;
        }

        /// <summary>
        /// Создание экземпляра Пожарного автомобиля с цистерной и лестницей.
        /// </summary>
        /// <param name="type">Тип Пожарного автомобиля.</param>
        /// <param name="brand">Производитель автомобиля.</param>
        /// <param name="model">Модель автомобиля.</param>
        /// <param name="yearOfCreation">Год выпуска.</param>
        /// <param name="numberOfSeats">Количество мест.</param>
        /// <param name="maxSpeed">Максимальная скорость.</param>
        /// <param name="fuelVolume">Объем бензобака.</param>
        /// <param name="fireEngine">Модель пожарной установки.</param>
        /// <param name="tankVolume">Объем цистерны.</param>
        /// <param name="foamVolume">Объем пенообразователя.</param>
        /// <param name="pumpCapacity">Производительность насоса.</param>
        /// <param name="liftingHeight">Высота стрелы.</param>
        public FireTruckBase(string type, string brand, string model, DateTime yearOfCreation, byte numberOfSeats, byte maxSpeed, int fuelVolume, string fireEngine, int tankVolume, int foamVolume, double pumpCapacity, int liftingHeight)
            : this(type, brand, model, yearOfCreation, numberOfSeats, maxSpeed, fuelVolume, fireEngine, tankVolume, foamVolume, pumpCapacity)
        {            
            LiftingHeight = liftingHeight;
        }

        /// <summary>
        /// Создание экземпляра Пожарного автомобиля с лестницей.
        /// </summary>
        /// <param name="type">Тип Пожарного автомобиля.</param>
        /// <param name="brand">Производитель автомобиля.</param>
        /// <param name="model">Модель автомобиля.</param>
        /// <param name="yearOfCreation">Год выпуска.</param>
        /// <param name="numberOfSeats">Количество мест.</param>
        /// <param name="maxSpeed">Максимальная скорость.</param>
        /// <param name="fuelVolume">Объем бензобака.</param>
        /// <param name="liftingHeight">Высота стрелы.</param>
        public FireTruckBase(string type, string brand, string model, DateTime yearOfCreation, byte numberOfSeats, byte maxSpeed, int fuelVolume, int liftingHeight)
            : this(brand, model, yearOfCreation, numberOfSeats, maxSpeed, fuelVolume)
        {
            if (string.IsNullOrEmpty(type))
            {
                throw new ArgumentException("Тип ПА не может быть пустым или иметь значение null", nameof(type));
            }
            LiftingHeight = liftingHeight;
        }


        public override string ToString()
        {
            return Type + Brand + "-" + Model;
        }

    }
}
