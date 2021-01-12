using System;
using System.Collections.Generic;
using System.Text;

namespace LookaukwatApp.Models.MobileModels
{
    public class ProductForMobileViewModel
    {
        public int id { get; set; }

        public string Title { get; set; }

        public int ViewNumber { get; set; }
        public string Town { get; set; }
        public string Category { get; set; }


        public int Price { get; set; }

        public DateTime DateAdd { get; set; }
        public string Date { get => ConvertDate(DateAdd); }
        public string Image { get; set; }

        private string ConvertDate(DateTime date)
        {
            TimeSpan elapsTime = DateTime.Now - date;
            string period = null;
            int time = 0;

            if (elapsTime.TotalMinutes < 60)
            {
                time = elapsTime.Minutes;
                time = -1 * time;
                period = "minutes";
            }
            else if (elapsTime.TotalMinutes > 60 && elapsTime.TotalMinutes < 1440)
            {
                time = elapsTime.Hours;
                period = "Heures";
            }
            else if (elapsTime.TotalMinutes > 1440)
            {
                time = elapsTime.Days;
                period = "Jours";
            }

            return "Ajoutée il y'a " + time.ToString() + " " + period;
        }
    }
}
