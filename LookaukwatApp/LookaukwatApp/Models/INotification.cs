using System;
using System.Collections.Generic;
using System.Text;

namespace LookaukwatApp.Models
{
    public interface INotification
    {
        void CreateNotification(string title, string message);
    }
}
