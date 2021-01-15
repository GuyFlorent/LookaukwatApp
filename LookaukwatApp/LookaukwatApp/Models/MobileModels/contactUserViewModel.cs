using System;
using System.Collections.Generic;
using System.Text;

namespace LookaukwatApp.Models.MobileModels
{
    public class contactUserViewModel 
    {
        public string NameSender { get; set; }

        public string SubjectSender { get; set; }

        public string EmailSender { get; set; }

        public string Message { get; set; }
        public string Linkshare { get; set; }
        public string RecieverEmail { get; set; }
        public string RecieverName { get; set; }
        //public string targetId { get; set; }
        //public HttpPostedFileBase file { get; set; }
        //public string attachFile { get; set; }
        public string Category { get; set; }
    }
}
