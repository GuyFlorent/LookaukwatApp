using System;
using System.Collections.Generic;
using System.Globalization;
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
        public string PriceConvert { get => Price.ToString("N", CultureInfo.CreateSpecificCulture("af-ZA")).Split(',')[0].Trim(); }
        public string Title { get; set; }

        public string Description { get; set; }
        public int ViewNumber { get; set; }
        public string Street { get; set; }

        public DateTime DateAdd { get; set; }
        public string Date { get; set; }


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
        public int NumberImages { get; set; }
        public List<SimilarProductViewModel> SimilarProduct { get; set; }
       
    }
}
