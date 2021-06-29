using FireForecasting.DAL.Entityes.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireForecasting.DAL.Entityes.Incidents
{
    public class Incident : Entity
    {
        /// <summary>
        /// Адрес происшествия.
        /// </summary>
        public string Adress { get; set; }
    }
}
