using FireForecasting.DAL.Entityes.Base;

namespace FireForecasting.DAL.Entityes.Departments
{
    /// <summary> Сотрудник. </summary>
    public class Employee:People
    {
        /// <summary> Подразделение. </summary>
        public virtual Division Division { get; set; }

        /// <summary> Должность сотрудника. </summary>
        public string Position { get; set; }

        /// <summary> Звание сотрудника. </summary>
        public string Rank { get; set; }
    }
}
