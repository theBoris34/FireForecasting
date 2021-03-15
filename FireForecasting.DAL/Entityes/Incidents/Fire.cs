﻿using FireForecasting.DAL.Entityes.Base;
using FireForecasting.DAL.Entityes.Departments;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FireForecasting.DAL.Entityes.Incidents
{
    public class Fire : Entity
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal CostOfDamage { get; set; }
        public string Adress { get; set; }

        public virtual Employee Employee {get;set;}
        public virtual Division Division { get; set; }

        public Fire()
        {

        }
        public Fire(decimal costOfDamage, string adress, Employee employee, Division division)
        {
            CostOfDamage = costOfDamage;
            Adress = adress;
            Employee = employee;
            Division = division;
        }

    }
}
