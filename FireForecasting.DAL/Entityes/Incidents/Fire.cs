using FireForecasting.DAL.Entityes.Base;
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

        public virtual ICollection<Employee> Employees {get;set;}

        public virtual ICollection<Division> Divisions { get; set; }

    }
}
