using FireForecasting.DAL.Entityes.Base;
using System.Collections.Generic;

namespace FireForecasting.DAL.Entityes.Departments
{
    public class Department:Entity
    {
        /// <summary> Название управления. </summary>
        public string Name { get; set; }

        /// <summary> Примечание. </summary>
        public string Note { get; set; }

        /// <summary> Список подразделений. </summary>
        public virtual ICollection<Division> Divisions { get; set; }
    }
}
