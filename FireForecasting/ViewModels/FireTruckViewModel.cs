using FireForecasting.DAL.Entityes.Departments;
using FireForecasting.Interfaces;
using MathCore.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireForecasting.ViewModels
{
    class FireTruckViewModel:ViewModel
    {

        private readonly IRepository<FireTruck> _FireTruckRepository;
        //public IEnumerable<FireTruck> FireTrucks = _FireTruckRepository.Items;

        public string Title { get; set; } = "Пожарная техника";

        public int FireTrucksBase { get; set; }

        public FireTruckViewModel(IRepository<FireTruck>  FireTruckRepository)
        {
            _FireTruckRepository = FireTruckRepository;
        }

    }
}
