using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace LookaukwatApp.Models.MobileModels
{
    public class MultimediaModelViewModel
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

        public string Rubrique { get; set; }

        public DateTime DateAdd { get; set; }
        public string Date { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }

        public string Capacity { get; set; }

        public string State { get; set; }
        public string UserName { get; set; }
        public string UserPhone { get; set; }
        public string UserEmail { get; set; }
        public List<string> Images { get; set; }
        public int NumberImages { get; set; }
        public List<SimilarProductViewModel> SimilarProduct { get; set; }
       
    }
}
