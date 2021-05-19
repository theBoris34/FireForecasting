using FireForecasting.DAL.Entityes.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireForecasting.DAL.Entityes.Departments
{
    /// <summary>
    /// Дежурная смена.
    /// </summary>
    public class DutyShift:Entity
    {

        public virtual Division Division { get; set; }
        /// <summary>
        /// Дата дежурства. 
        /// </summary>
        public DateTime Date { get; set; }  //TODO: СДЕЛАТЬ РАСЧЕТ ВРЕМЕНИ С 8 до 8.

        /// <summary>
        /// Список Расчетов на ПА.
        /// </summary>
        public List<FireBrigade> FireBrigades { get; set; }

        /// <summary>
        /// Диспетчер.
        /// </summary>
        public Employee Dispatcher { get; set; }

        /// <summary>
        /// Начальник дежуной смены (начальник караула)
        /// </summary>
        public Employee ShiftSupervisor { get; set; }

        public DutyShift(DateTime date, List<FireBrigade> fireBrigades, Employee dispatcher, Employee shiftSupervisor)
        {
            Date = date;
            FireBrigades = fireBrigades;
            Dispatcher = dispatcher;
            ShiftSupervisor = shiftSupervisor;
        }

    }
}
