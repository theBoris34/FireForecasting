using FireForecasting.Models.Interface;
using System.Collections.Generic;

namespace FireForecasting.Models.Departments
{
    /// <summary> Подразделение. </summary>
    class Division:IEntity
    {
        /// <summary> Id подразделения. </summary>
        public int Id { get; set; }

        /// <summary> Управление. </summary>
        public Department Department { get; set; }

        /// <summary> Название подразделения. </summary>
        public string Name { get; set; }

        /// <summary> Примечание </summary>
        public string Note { get; set; }

        /// <summary> Список сотрудников. </summary>
        public ICollection<Employee> Employees { get; set; }
    }
}
