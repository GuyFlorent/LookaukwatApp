using System;
using System.Collections.Generic;
using System.Text;

namespace LookaukwatApp.Models
{
    public class ApartmentRentalModel : ProductModel
    {
        public int ApartSurface { get; set; }
        public int RoomNumber { get; set; }
        public string FurnitureOrNot { get; set; }
        public string Type { get; set; }
    }
}
