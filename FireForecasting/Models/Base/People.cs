using FireForecasting.Models.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireForecasting.Models.Base
{
    /// <summary> Базовый класс людей. </summary>
        class People:IEntity
    {
        /// <summary> ID человека. </summary>
        public int Id { get; set; }
        /// <summary> Имя. </summary>
        public string Name { get; set; }

        /// <summary> Фамилия. </summary>
        public string Surname { get; set; }

        /// <summary> Отчество. </summary>
        public string Patronymic { get; set; }

        /// <summary> Дата рождения. </summary>
        public DateTime Birthday { get; set; }

        /// <summary> Номер телефона. </summary>
        public string Phone { get; set; }

        /// <summary> Примечание. </summary>
        public string Note { get; set; } = "";
        
    }
}
