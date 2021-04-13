using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireForecasting.ViewModels
{
    class ViewModelLocator
    {
        public MainWindowViewModel MainWindowModel => App.Services.GetRequiredService<MainWindowViewModel>();
        public IncidentsViewModel IncidentsViewModel => App.Services.GetRequiredService<IncidentsViewModel>();
    }
}
