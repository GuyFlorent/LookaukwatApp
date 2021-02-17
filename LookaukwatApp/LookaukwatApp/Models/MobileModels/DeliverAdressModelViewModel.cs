using System;
using System.Collections.Generic;
using System.Text;

namespace LookaukwatApp.Models.MobileModels
{
   
    public class DeliverAdressModelViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string Town { get; set; }
        public string Number { get; set; }
        public string Telephone { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public double Distance { get; set; }
    }
}
