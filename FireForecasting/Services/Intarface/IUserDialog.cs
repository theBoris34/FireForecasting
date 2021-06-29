using FireForecasting.DAL.Entityes.Departments;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireForecasting.Services.Intarface
{
    internal interface IUserDialog
    {
        bool Edit(Employee employee, Division selectedDivision);

        bool ConfirmInformation(string Information, string Caption);
        bool ConfirmWarning(string Warning, string Caption);
        bool ConfirmError(string Error, string Caption);
    }
}
