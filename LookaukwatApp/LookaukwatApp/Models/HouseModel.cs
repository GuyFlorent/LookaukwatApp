using System;
using System.Collections.Generic;
using System.Text;

namespace LookaukwatApp.Models
{
    public class HouseModel : ProductModel
    {
        public string RubriqueHouse { get; set; }
        public string TypeHouse { get; set; }
        public string FabricMaterialeHouse { get; set; }
        public string ColorHouse { get; set; }
        public string StateHouse { get; set; }
        public int Stock { get; set; }
    }
}
