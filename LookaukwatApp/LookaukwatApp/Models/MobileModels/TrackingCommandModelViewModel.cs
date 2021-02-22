using System;
using System.Collections.Generic;
using System.Text;

namespace LookaukwatApp.Models.MobileModels
{
    public class TrackingCommandModelViewModel
    {
        public string Town { get; set; }
        public string Street { get; set; }
        public string Road { get; set; }
        public DateTime Date { get; set; }
        public string DateLetter { get => Date.ToString("dddd, dd MMMM yyyy HH: mm:ss"); }
        public string Lat { get; set; }
        public string Lon { get; set; }
    }
}
