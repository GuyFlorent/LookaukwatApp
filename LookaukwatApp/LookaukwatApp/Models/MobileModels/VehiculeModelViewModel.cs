using System;
using System.Collections.Generic;
using System.Text;

namespace LookaukwatApp.Models.MobileModels
{
    public class VehiculeModelViewModel
    {
        public int id { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public string SearchOrAsk { get; set; }

        public string Town { get; set; }

        public int Price { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
        public int ViewNumber { get; set; }
        public string Street { get; set; }

        public DateTime DateAdd { get; set; }
        public string Date { get => ConvertDate(DateAdd); }


        public string RubriqueVehicule { get; set; }

        public string BrandVehicule { get; set; }

        public string ModelVehicule { get; set; }

        public string TypeVehicule { get; set; }

        public string PetrolVehicule { get; set; }

        public string FirstYearVehicule { get; set; }

        public string YearVehicule { get; set; }

        public string MileageVehicule { get; set; }

        public string NumberOfDoorVehicule { get; set; }

        public string ColorVehicule { get; set; }

        public string StateVehicule { get; set; }

        public string GearBoxVehicule { get; set; }
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
