using MathCore.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireForecasting.ViewModels
{
    class FireEditorViewModel:ViewModel
    {
        #region Title - Заголовок окна
        private string _Title = "Редактирование пожаров";

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        #region TimeMessage - Время вызова
        private string _TimeMessage = "00:00";

        public string TimeMessage
        {
            get => _TimeMessage;
            set => Set(ref _TimeMessage, value);
        }
        #endregion

        #region Address - Заголовок окна
        private string _Address;

        public string Address
        {
            get => _Address;
            set => Set(ref _Address, value);
        }
        #endregion    

        #region RankFire - Ранг пожара
        private string _RankFire = "1";

        public string RankFire
        {
            get => _RankFire;
            set => Set(ref _RankFire, value);
        }
        #endregion  
        
        #region RanksFire - Список рангов пожара
        private List<string> _RanksFire;

        public List<string> RanksFire
        {
            get => _RanksFire;
            set => Set(ref _RanksFire, value);
        }
        #endregion

        #region DescriptionOfFire - Описание пожара
        private string _DescriptionOfFire;

        public string DescriptionOfFire
        {
            get => _DescriptionOfFire;
            set => Set(ref _DescriptionOfFire, value);
        }
        #endregion

        #region DescriptionOfFire - Описание пожара
        private string _DescriptionOfFire;

        public string DescriptionOfFire
        {
            get => _DescriptionOfFire;
            set => Set(ref _DescriptionOfFire, value);
        }
        #endregion


        public FireEditorViewModel()
        {
            RanksFire = new List<string>()                    //TODO: Вывести из модели-представления
            {                                                 //TODO: Вывести из модели-представления
                "1","1-Бис", "2", "3", "4", "5"               //TODO: Вывести из модели-представления
            };                                                //TODO: Вывести из модели-представления




        //public Fire(DateTime date, string region, string adress, string rankOfFire, string DescriptionOfFire, string causeOfFire, decimal costOfDamage, decimal costOfSaved, Employee employee, Division division)
        }
    }
}
