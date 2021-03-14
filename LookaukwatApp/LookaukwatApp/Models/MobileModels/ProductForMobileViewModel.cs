using LookaukwatApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace LookaukwatApp.Models.MobileModels
{
    public class ProductForMobileViewModel  : BaseViewModel
    {
        public int id { get; set; }

        public string Title { get; set; }

        public int ViewNumber { get; set; }
        public int CallNumber { get; set; }
        public int MessageNumber { get; set; }
        public string Town { get; set; }
        public string Category { get; set; }
        public string HomeMessage { get => LooaukwatMessage(); }

        public bool IsLookaukwat { get; set; }
        public int Stock { get; set; }

        
        public int Price { get; set; }
        public string PriceConvert { get => Price.ToString("N", CultureInfo.CreateSpecificCulture("af-ZA")).Split(',')[0].Trim() ; }
        public DateTime DateAdd { get; set; }
        public string Date { get; set; }
        public string DateLetter { get; set; }
        public string Image { get; set; }
        public int NumberImages { get; set; }


        private string blackHeart = "heart_black";
        public string BlackHeart
        {
            get => blackHeart;
            set => SetProperty(ref blackHeart, value);
        }

        private string redHeart = "heart_red";
        public string   RedHeart
        {
            get => redHeart;
            set => SetProperty(ref redHeart, value);
        }

        private string LooaukwatMessage()
        {
           if(Category == "Emploi")
            {
                return "Garantie/Postuler en ligne";
            }else if(Category == "Immobilier")
            {
                return "Garantie Lookaukwat";
            }
            else
            {
                return "Garantie/Livraison possible";
            }
        }
      
    }
}
