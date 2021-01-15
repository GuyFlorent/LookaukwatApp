using LookaukwatApp.Models.MobileModels;
using LookaukwatApp.Services;
using LookaukwatApp.ViewModels.Message;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LookaukwatApp.Views.MessageView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactEmailUserPage 
    {
        ApiServices _apiServices = new ApiServices();
        ContactEmailUserViewModel Contact;

        public ContactEmailUserPage()
        {
            InitializeComponent();
        }
        public ContactEmailUserPage(contactUserViewModel contact)
        {
            InitializeComponent();
            Indicator.IsVisible = false;
            Name.Text = contact.NameSender;
            Email.Text = contact.EmailSender;
            Subject.Text = contact.SubjectSender;
            updateMessage(contact.Category, contact.RecieverName, contact.NameSender);
            Contact = new ContactEmailUserViewModel()
            {
                NameSender = contact.NameSender,
                EmailSender = contact.EmailSender,
                Category = contact.Category,
                Linkshare = contact.Linkshare,
                RecieverEmail = contact.RecieverEmail,
                RecieverName = contact.RecieverName,
                SubjectSender =contact.SubjectSender
            };
        }

        private async void ClosePoppup_Button(object o, EventArgs e)
        {
            await PopupNavigation.Instance.PopAllAsync();
           
        }

        private async void SendMessage_Button(object o, EventArgs e)
        {
            Indicator.IsVisible = true;
            Indicator.IsRunning = true;

            Contact.NameSender = Name.Text;
            Contact.EmailSender = Email.Text;
            Contact.SubjectSender = Subject.Text;
            Contact.Message = Description.Text;
            await _apiServices.SendMessageToPublisherAsync(Contact);

            Indicator.IsVisible = false;
            Indicator.IsRunning = false;

            await DisplayAlert("Alerte", "Votre annonce a été envoyé avec succès. Merci de votre confiance", "Ok");
            await PopupNavigation.Instance.PopAllAsync();
        }

        private void updateMessage(string categori, string ReceiverName, string senderName)
        {
            switch (categori)
            {
                case "Emploi":
                    Description.Text = "Bonjour " + ReceiverName + ", votre annonce m'intéresse beaucoup ! Est-elle toujours disponible ? Merci. Cordialement, " + senderName + ".";

                    break;

                case "Immobilier":
                    Description.Text = "Bonjour " + ReceiverName + ", votre annonce m'intéresse beaucoup ! Est-elle toujours disponible ? Merci. Cordialement, " + senderName + ".";

                    break;

                case "House":
                    Description.Text = "Bonjour " + ReceiverName + ", votre annonce m'intéresse beaucoup ! Est-elle toujours disponible ? Merci. Cordialement, " + senderName + ".";

                    break;

                case "Mode":
                    Description.Text = "Bonjour " + ReceiverName + ", votre annonce m'intéresse beaucoup ! Est-elle toujours disponible ? Merci. Cordialement, " + senderName + ".";
                    
                    break;

                case "Maison":
                    Description.Text = "Bonjour " + ReceiverName + ", votre annonce m'intéresse beaucoup ! Est-elle toujours disponible ? Merci. Cordialement, " + senderName + ".";

                    break;

                case "Vehicule":
                    Description.Text = "Bonjour " + ReceiverName + ", votre annonce m'intéresse beaucoup ! Est-elle toujours disponible ? Merci. Cordialement, " + senderName + ".";

                    break;
            }
        }
       
    }
}