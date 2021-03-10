using System;
using System.Collections.Generic;
using System.Text;
using MathCore.WPF.ViewModels;

namespace FireForecasting.ViewModels
{
    class MainWindowViewModel:ViewModel
    {
        private string _Title = "Главное окно программы!";

        public string Title { get=>_Title; set => Set(ref _Title,value); }
    }
}
