using FireForecasting.DAL.Entityes.Departments;
using FireForecasting.Services.Intarface;
using FireForecasting.ViewModels;
using FireForecasting.Views.Windows;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace FireForecasting.Services
{
    internal class UserDialogService:IUserDialog
    {
        public bool ConfirmInformation(string Information, string Caption) => MessageBox
            .Show(Information, Caption, 
                MessageBoxButton.YesNo, 
                MessageBoxImage.Information) 
                == MessageBoxResult.Yes;

        public bool ConfirmWarning(string Warning, string Caption) => MessageBox
           .Show(Warning, Caption,
               MessageBoxButton.YesNo,
               MessageBoxImage.Warning)
               == MessageBoxResult.Yes;

        public bool ConfirmError(string Error, string Caption) => MessageBox
           .Show(Error, Caption,
               MessageBoxButton.OK,
               MessageBoxImage.Error)
               == MessageBoxResult.OK;


        public bool Edit(Employee employee)
        {
            var employee_editor_model = new EmployeeEditorViewModel(employee);
            var employee_editor_window = new EmployeeEditorWindow
            {
                DataContext = employee_editor_model
            };

            if (employee_editor_window.ShowDialog() != true) return false;

            employee.Surname = employee_editor_model.Surname;
            employee.Name = employee_editor_model.Name;
            employee.Patronymic = employee_editor_model.Patronymic;
            employee.Rank = employee_editor_model.Rank;

            return true;
        }
        
    }
}
