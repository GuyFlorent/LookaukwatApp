using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LookaukwatApp.ViewModels.Message
{
 
    public class ContactEmailUserViewModel : BaseViewModel
    {
        private string nameSender;
        public string NameSender
        {
            get { return nameSender; }
            set { SetProperty(ref nameSender, value); }
        }
        private string subjectSender;
        public string SubjectSender
        {
            get { return subjectSender; }
            set { SetProperty(ref subjectSender, value); }
        }
        private string emailSender;
        public string EmailSender
        {
            get { return emailSender; }
            set { SetProperty(ref emailSender, value); }
        }
        private string message;
        public string Message
        {
            get { return message; }
            set { SetProperty(ref message, value); }
        }
        private string linkshare;
        public string Linkshare
        {
            get { return linkshare; }
            set { SetProperty(ref linkshare, value); }
        }
        private string recieverEmail;
        public string RecieverEmail
        {
            get { return recieverEmail; }
            set { SetProperty(ref recieverEmail, value); }
        }
        private string recieverName;
        public string RecieverName
        {
            get { return recieverName; }
            set { SetProperty(ref recieverName, value); }
        }
        private string category;
        public string Category
        {
            get { return category; }
            set { SetProperty(ref category, value); }
        }
    }
}
