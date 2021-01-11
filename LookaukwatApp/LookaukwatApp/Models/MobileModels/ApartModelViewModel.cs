using System;
using System.Collections.Generic;
using System.Text;

namespace LookaukwatApp.Models.MobileModels
{
    public class ApartModelViewModel
    {

        public int id { get; set; }

        public string SearchOrAsk { get; set; }

        public string Town { get; set; }

        public int Price { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Street { get; set; }

        public int ApartSurface { get; set; }

        public DateTime DateAdd { get; set; }
        public string Date { get => ConvertDate(DateAdd); }
        public string Type { get; set; }

        public int RoomNumber { get; set; }

        public string FurnitureOrNot { get; set; }
        public string UserName { get; set; }
        public string UserPhone { get; set; }
        public string UserEmail { get; set; }
        public List<string> Images { get; set; }
        public List<SimilarProductViewModel> SimilarProduct { get; set; }

        private string ConvertDate(DateTime date)
        {
            TimeSpan elapsTime = DateTime.Now - date;
            string period = null;
            int time = 0;

            if (elapsTime.TotalMinutes < 60)
            {
                time = elapsTime.Minutes;
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