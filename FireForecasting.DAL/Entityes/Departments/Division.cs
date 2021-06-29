using FireForecasting.DAL.Entityes.Base;
using System.Collections.Generic;

namespace FireForecasting.DAL.Entityes.Departments
{
    /// <summary> Подразделение. </summary>
    public class Division:Entity
    {
        public virtual List<DutyShift> DutyShifts { get; set; }
        /// <summary> Управление. </summary>
        public virtual Department Department { get; set; }

        /// <summary> Название подразделения. </summary>
        public string Name { get; set; }

        /// <summary> Примечание </summary>
        public string Note { get; set; }

        /// <summary> Список сотрудников. </summary>
        public virtual ICollection<Employee> Employees { get; set; }

        /// <summary> Список техники. </summary>
        public virtual ICollection<FireTruck> FireTrucks { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
