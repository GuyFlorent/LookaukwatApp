using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace LookaukwatApp.Models.MobileModels
{
    public class SimilarProductViewModel : LookaukwatApp.ViewModels.BaseViewModel
    {
        public int id { get; set; }
        public string Title { get; set; }

        public string Town { get; set; }

        public int Price { get; set; }
        public string PriceConvert { get => Price.ToString("N", CultureInfo.CreateSpecificCulture("af-ZA")).Split(',')[0].Trim(); }
        public DateTime DateAdd { get; set; }
        public string Date { get; set; }

        public string Category { get; set; }
        public bool IsLookaukwat { get; set; }
        public string Image { get; set; }
        public int NumberImages { get; set; }


        private string blackHeart = "heart_black";
        public string BlackHeart
        {
            get => blackHeart;
            set => SetProperty(ref blackHeart, value);
        }

        private string redHeart = "heart_red";
        public string RedHeart
        {
            get => redHeart;
            set => SetProperty(ref redHeart, value);
        }
    }
}