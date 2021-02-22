﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LookaukwatApp.Models
{
     public class TrackingCommandModel
    {
        public int Id { get; set; }
        public virtual CommandModel Command { get; set; }
        public virtual ApplicationUser UserAgent { get; set; }
        public string Town { get; set; }
        public string Street { get; set; }
        public DateTime Date { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
    }
}
