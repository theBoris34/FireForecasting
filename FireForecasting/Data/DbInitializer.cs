using FireForecasting.DAL.Context;
using FireForecasting.DAL.Entityes.Base;
using FireForecasting.DAL.Entityes.Departments;
using FireForecasting.DAL.Entityes.Incidents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireForecasting.Data
{
    class DbInitializer
    {
        private readonly DepartmentDB _db;
        private readonly ILogger<DbInitializer> _Logger;

        public DbInitializer(DepartmentDB db, ILogger<DbInitializer> Logger)
        {
            _db = db;
            _Logger = Logger;
        }

        public async Task InitializeAsync()
        {
            var timer = Stopwatch.StartNew();
            _Logger.LogInformation("Инициализация БД...");
            //_Logger.LogInformation("Удаление существующей БД...");
            //await _db.Database.EnsureDeletedAsync().ConfigureAwait(false);
            //_Logger.LogInformation("Удаление существующей БД выполнено за {0} мс", timer.ElapsedMilliseconds);
            _Logger.LogInformation("Миграция БД...");
            await _db.Database.MigrateAsync();
            _Logger.LogInformation("Миграция БД выполнено за {0} мс", timer.ElapsedMilliseconds);

            if (await _db.Employees.AnyAsync()) return;

            //await InitializeDepartments();
            //await InitializeDivisions();
            //await InitializeEmployees();
            //await InitializeFires();
            //await InitializeFireTruckBase();
            //await InitializeFireTruck();


            _Logger.LogInformation("Инициализация БД выполнена за {0} с", timer.Elapsed.TotalSeconds);
        }

        private const int __DepartmentCount = 5;

        private Department[] _Departments;

        public async Task InitializeDepartments()
        {
            var timer = Stopwatch.StartNew();
            _Logger.LogInformation("Инициализация управлений...");
            _Departments = new Department[__DepartmentCount];
            for (var i = 0; i < __DepartmentCount; i++)
                _Departments[i] = new Department { Name = $"Управление {i + 1}" };

            await _db.Departments.AddRangeAsync(_Departments);
            await _db.SaveChangesAsync();


            _Logger.LogInformation("Инициализация управлений выполнена за {0} мс", timer.ElapsedMilliseconds);
        }

        private const int __DivisionCount = 20;

        private Division[] _Divisions;
        public async Task InitializeDivisions()
        {
            var timer = Stopwatch.StartNew();
            _Logger.LogInformation("Инициализация отделов...");

            var Rnd = new Random();
            _Divisions = Enumerable.Range(1, __DivisionCount)
                .Select(i => new Division
                {
                    Name = $"Дивизион {i + 1}",
                    Department = _Departments[Rnd.Next(1, __DepartmentCount)] 
                })
                .ToArray();

            await _db.Divisions.AddRangeAsync(_Divisions);
            await _db.SaveChangesAsync();
            
            _Logger.LogInformation("Инициализация отделов выполнена за {0} мс", timer.ElapsedMilliseconds);
        }

        private const int __EmployeeCount = 100;

        private Employee[] _Employees;
        public async Task InitializeEmployees()
        {
            var timer = Stopwatch.StartNew();
            _Logger.LogInformation("Инициализация сотрудников...");

            var Rnd = new Random();
            _Employees = Enumerable.Range(1, __EmployeeCount)
                .Select(i => new Employee
                {                                                               
                    Name = $"Имя {i + 1}",
                    Surname = $"Фамилия {i + 1}",
                    Patronymic = $"Отчество {i + 1}",
                    Birthday = DateTime.Now,
                    Phone = new HashCode().ToString(),
                    Position = new HashCode().ToString(),
                    Rank = new HashCode().ToString(),
                    Division = _Divisions[Rnd.Next(1, __DivisionCount)]
                })
                .ToArray();

            await _db.Employees.AddRangeAsync(_Employees);
            await _db.SaveChangesAsync();

            _Logger.LogInformation("Инициализация сотрудников выполнена за {0} мс", timer.ElapsedMilliseconds);
        }

        private const int __FireCount = 500;
        
        
        public async Task InitializeFires()
        {
            var timer = Stopwatch.StartNew();
            _Logger.LogInformation("Инициализация пожаров...");

            var Rnd = new Random();
            var Fires = Enumerable.Range(1, __FireCount)
                .Select(i => new Fire
                {
                    Date = DateTime.Now,
                    Region = $"{i + 1} район",
                    Adress = $"Адрес {i + 1}",
                    RankOfFire = $"Ранг пожара {Rnd.Next(1,4)}",
                    DescriptionOfFire = new HashCode().ToString(),
                    CauseOfFire = $"Причина пожара {Rnd.Next(1, 4)}",
                    CostOfDamage = Rnd.Next(10_000, 500_000_000),
                    CostOfSaved = Rnd.Next(10_000, 500_000_000),
                    Division =_Divisions[Rnd.Next(1, __DivisionCount)],
                    Employee = _Employees[Rnd.Next(1, __EmployeeCount)],
                });

            await _db.Fires.AddRangeAsync(Fires);
            await _db.SaveChangesAsync();
            
            _Logger.LogInformation("Инициализация пожаров выполнена за {0} с", timer.Elapsed.TotalSeconds);
        }



        private const int __FireTruckBaseCount = 10;
        private FireTruckBase[] _FireTruckBase;
        public async Task InitializeFireTruckBase()
        {
            var timer = Stopwatch.StartNew();
            _Logger.LogInformation("Инициализация базовых пожарных автомобилей...");

            var Rnd = new Random();
            _FireTruckBase = Enumerable.Range(1, __FireTruckBaseCount)
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

            await _db.FireTruckBase.AddRangeAsync(_FireTruckBase);
            await _db.SaveChangesAsync();

            _Logger.LogInformation("Инициализация базовых пожарных автомобилей выполнена за {0} с", timer.Elapsed.TotalSeconds);
        }


        private const int __FireTruckCount = __DivisionCount*2;
        private FireTruck[] _FireTruck;
        public async Task InitializeFireTruck()
        {
            var timer = Stopwatch.StartNew();
            _Logger.LogInformation("Инициализация пожарных автомобилей...");
            _FireTruck = new FireTruck[__FireTruckCount];
            var Rnd = new Random();
            for (var i = 0; i < __FireTruckCount; i++)
                _FireTruck[i] = new FireTruck(_FireTruckBase[Rnd.Next(1, __FireTruckBaseCount)], _Divisions[Rnd.Next(1, __DivisionCount)]);

            await _db.FireTruck.AddRangeAsync(_FireTruck);
            await _db.SaveChangesAsync();

            _Logger.LogInformation("Инициализация пожарных автомобилей выполнена за {0} с", timer.Elapsed.TotalSeconds);
        }

    }
}
