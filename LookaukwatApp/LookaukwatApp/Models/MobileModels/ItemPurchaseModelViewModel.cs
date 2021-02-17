using System;
using System.Collections.Generic;
using System.Text;

namespace LookaukwatApp.Models.MobileModels
{
   public class ItemPurchaseModelViewModel
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }
        public string TotalPrice { get; set; }
        public string DeliveredPrice { get; set; }
        public string PayementMethod { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
    }
}
