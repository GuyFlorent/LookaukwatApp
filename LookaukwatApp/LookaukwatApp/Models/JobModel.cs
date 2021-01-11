using LookaukwatApp.Models.MobileModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace LookaukwatApp.Models
{
    public class JobModel : ProductModel
    {

        public string TypeContract { get; set; }

        public string ActivitySector { get; set; }

        public List<SimilarProductViewModel> SimilarProduct { get; set; }

    }
}
