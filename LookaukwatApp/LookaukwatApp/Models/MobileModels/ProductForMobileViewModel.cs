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
        public string Date { get; set; }
        public string DateLetter { get; set; }
        public string Image { get; set; }

      
    }
}
