using FireForecasting.Models.Base;
using FireForecasting.Models.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireForecasting.Models.Departments
{
    /// <summary> Сотрудник. </summary>
    class Employee:People
    {
        /// <summary> Подразделение. </summary>
        public Division  Division { get; set; }

        /// <summary> Должность сотрудника. </summary>
        public string Position { get; set; }

        /// <summary> Звание сотрудника. </summary>
        public string Rank { get; set; }

    }
}
