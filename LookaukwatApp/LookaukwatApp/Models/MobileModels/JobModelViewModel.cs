using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace LookaukwatApp.Models.MobileModels
{
    public class JobModelViewModel
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string Town { get; set; }

        public string Street { get; set; }

        public int Price { get; set; }
        public string PriceConvert { get => Price.ToString("N", CultureInfo.CreateSpecificCulture("af-ZA")).Split(',')[0].Trim(); }
        public string Date { get; set; }

        public string SearchOrAskJob { get; set; }
        public DateTime DateAdd { get; set; }
        public int ViewNumber { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
       
        public string TypeContract { get; set; }
       
        public string ActivitySector { get; set; }
        public  ApplicationUser User { get; set; }
        public List<SimilarProductViewModel> SimilarProduct { get; set; }
        public List<ImageProcductModel> Images { get; set; }
        public int NumberImages { get; set; }
    }
}
