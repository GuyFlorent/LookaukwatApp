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
        public int Stock { get; set; }
        public string TotalPrice { get; set; }
        public int TotalPrice_int { get; set; }
        public string DeliveredPrice { get; set; }
        public int DeliveredPrice_int { get; set; }
        public string PayementMethod { get; set; }
        public int Quantity { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public string Town { get; set; }
        public string Category { get; set; }
    }
}
