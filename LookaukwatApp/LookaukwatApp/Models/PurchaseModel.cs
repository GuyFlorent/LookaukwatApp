using System;
using System.Collections.Generic;
using System.Text;

namespace LookaukwatApp.Models
{
     public class PurchaseModel
    {
        public int Id { get; set; }
        public virtual CommandModel Command { get; set; }
        public virtual ProductModel product { get; set; }
        public int Quantities { get; set; }
    }
}
