using FireForecasting.DAL.Entityes.Base;
using FireForecasting.DAL.Entityes.Departments;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace FireForecasting.CONSOL
{
    class Program
    {
        static void Main(string[] args)
        {
            var Rnd = new Random();
            int __DepartmentCount = 5;
            int __DivisionCount = 20;

            Department[] _Departments;
            _Departments = new Department[__DepartmentCount];
            for (var i = 0; i < __DepartmentCount; i++)
                _Departments[i] = new Department { Name = $"Управление {i + 1}" };

            Division[] _Divisions;

            _Divisions = Enumerable.Range(1, __DivisionCount)
                .Select(i => new Division
                {
                    Name = $"Дивизион {i + 1}",
                    Department = _Departments[Rnd.Next(1, __DepartmentCount)]
                })
                .ToArray();


            FireTruckBase[] _FireTruckBase;

            _FireTruckBase = Enumerable.Range(1, 10)
                    .Select(i => new FireTruckBase
                    {
                        Type = $"Тип ПА {i}",
                        Brand = $"Бренд ПА {i}",
                        Model = $"Модель ПА {i}",
                        YearOfCreation = DateTime.Now,
                        NumberOfSeats = (byte)Rnd.Next(1, 7),
                        MaxSpeed = (byte)Rnd.Next(50, 100),
                        FuelVolume = Rnd.Next(50, 70),
                        FireEngine = $"Модель установки {i}",
                        TankVolume = (float)Rnd.Next(1, 5),
                        FoamVolume = (float)Rnd.Next(1, 5),
                        PumpCapacity = Rnd.Next(40, 80),
                        LiftingHeight = (byte)Rnd.Next(30, 70)
                    })
                    .ToArray();
            FireTruck[] _FireTruck = new FireTruck[20];

            //_FireTruck = Enumerable.Range(1, 10)
            //            .Select(i => new FireTruck
            //            {
                            
            //                Division = _Divisions[Rnd.Next(1, __DivisionCount)]
            //            })
            //            .ToArray();

            for (var i = 0; i < 20; i++)
                _FireTruck[i] = new FireTruck(_FireTruckBase[Rnd.Next(1,_FireTruckBase.Length)], _Divisions[Rnd.Next(1, __DivisionCount)]);

            Console.WriteLine("Hello World!");
            Console.ReadLine();

        }        

    }
}