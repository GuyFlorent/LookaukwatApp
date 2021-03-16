using System;
using System.Collections.Generic;
using System.Text;

namespace LookaukwatApp.Models
{
    public class EventModel: ProductModel
    {
        public string RubriqueEvent { get; set; }
        public string TypeEvent { get; set; }
        public string Sport_Game { get; set; }
        public string Artist_Name { get; set; }
        public DateTime DateEvent { get; set; }
        public TimeSpan Hour { get; set; }
    }
}
