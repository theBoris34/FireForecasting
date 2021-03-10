using FireForecasting.Models.Interface;
using System.Collections.Generic;

namespace FireForecasting.Models.Departments
{
    class Department:IEntity
    {
        /// <summary> Id управления. </summary>
        public int Id { get; set; }

        /// <summary> Название управления. </summary>
        public string Name { get; set; }

        /// <summary> Примечание. </summary>
        public string Note { get; set; }

        /// <summary> Список подразделений. </summary>
        public ICollection<Division> Divisions { get; set; }
    }
}
