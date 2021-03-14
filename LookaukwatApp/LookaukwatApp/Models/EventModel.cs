using System;
using System.Collections.Generic;
using System.Text;

namespace LookaukwatApp.Models
{
    public class EventModel: ProductModel
    {
        public string Rubrique { get; set; }
        public string Type { get; set; }
        public string Sport_Game { get; set; }
        public string Artist_Name { get; set; }
        public DateTime Date { get; set; }
    }
}
