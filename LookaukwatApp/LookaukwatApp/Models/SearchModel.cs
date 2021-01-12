using System;
using System.Collections.Generic;
using System.Text;

namespace LookaukwatApp.Models
{
    public class SearchModel
    {
        //For all product
        public string Category { get; set; }
        public string Town { get; set; }
        public string SearchOrAskJob { get; set; }


        //For Job
        public int PriceJob { get; set; }
        public string TypeContract { get; set; }
        public string ActivitySector { get; set; }

        //For Mode

        public int PriceMode { get; set; }
        public string RubriqueMode { get; set; }
        public string TypeMode { get; set; }
        public string BrandMode { get; set; }
        public string UniversMode { get; set; }
        public string SizeMode { get; set; }
        public string State { get; set; }
        public string ColorMode { get; set; }

        //House

        public int PriceHouse { get; set; }
        public string RubriqueHouse { get; set; }
        public string TypeHouse { get; set; }
        public string FabricMaterialHouse { get; set; }
        public string StateHouse { get; set; }
        public string ColorHouse { get; set; }

        //Multimedia
        public int PriceMulti { get; set; }
        public string MultimediaRubrique { get; set; }
        public string MultimediaBrand { get; set; }
        public string MultimediaModel { get; set; }
        public string MultimediaCapacity { get; set; }

        //Apart

        public int PriceApart { get; set; }
        public int RoomNumberAppart { get; set; }
        public string FurnitureOrNotAppart { get; set; }
        public string TypeAppart { get; set; }
        public int ApartSurfaceAppart { get; set; }

        //Vehicule

        public int PriceVehicule { get; set; }
        public string VehiculeBrand { get; set; }
        public string VehiculeModel { get; set; }
        public string VehiculeType { get; set; }
        public string Petrol { get; set; }
        public string Year { get; set; }
        public string Mileage { get; set; }
        public string NumberOfDoor { get; set; }
        public string GearBox { get; set; }
        public string VehiculeState { get; set; }
        public string Color { get; set; }
    }
}
