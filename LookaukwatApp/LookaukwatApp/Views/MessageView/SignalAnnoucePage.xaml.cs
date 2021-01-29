using LookaukwatApp.Models.MobileModels;
using LookaukwatApp.Services;
using LookaukwatApp.ViewModels.Message;
using LookaukwatApp.ViewModels.StaticList;
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
	public partial class SignalAnnoucePage : ContentPage
	{
        ApiServices _apiServices = new ApiServices();
        ContactEmailUserViewModel Contact;
        private string selectedItem = null;
        public string SelectedItem { get => selectedItem; set => selectedItem = value; }
        public SignalAnnoucePage ()
		{
			InitializeComponent ();
		}

		public SignalAnnoucePage(contactUserViewModel contact)
		{
			InitializeComponent();
            Shell.SetTabBarIsVisible(this, false);

            Indicator.IsVisible = false;
            Name.Text = contact.NameSender;
            Email.Text = contact.EmailSender;
            TextMotif.ItemsSource = StaticListViewModel.SignalAnnouceMotifList;

            Contact = new ContactEmailUserViewModel()
            {
                NameSender = contact.NameSender,
                EmailSender = contact.EmailSender,
                Category = contact.Category,
                Linkshare = contact.Linkshare,
                RecieverEmail = contact.RecieverEmail,
                RecieverName = contact.RecieverName,
                SubjectSender = contact.SubjectSender
            };
        }

        private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            Picker picker = sender as Picker;
            SelectedItem = picker.SelectedItem.ToString(); // This is the model selected in the picker
        }

        private async void SendMessage_Button(object o, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SelectedItem))
            {
                LabelMotif.TextColor = Color.Red;
            }else
            {
                LabelMotif.TextColor = Color.Black;
            }
            if (string.IsNullOrWhiteSpace(Name.Text))
            {
                LabelName.TextColor = Color.Red;
            }
            else
            {
                LabelName.TextColor = Color.Black;
            }
            if (string.IsNullOrWhiteSpace(Email.Text))
            {
                LabelEmail.TextColor = Color.Red;
            }
            else
            {
                LabelEmail.TextColor = Color.Black;
            }

            if (string.IsNullOrWhiteSpace(Description.Text))
            {
                LabelDescription.TextColor = Color.Red;
            }
            else
            {
                LabelDescription.TextColor = Color.Black;
            }

            if(!string.IsNullOrWhiteSpace(SelectedItem) && !string.IsNullOrWhiteSpace(Name.Text) 
                && !string.IsNullOrWhiteSpace(Email.Text) && !string.IsNullOrWhiteSpace(Description.Text))
            {
                Indicator.IsVisible = true;
                Indicator.IsRunning = true;

                Contact.NameSender = Name.Text;
                Contact.EmailSender = Email.Text;
                Contact.SubjectSender = TextMotif.SelectedItem.ToString();
                Contact.Message = Description.Text;
                await _apiServices.SendMessageToPublisherAsync(Contact);

                Indicator.IsVisible = false;
                Indicator.IsRunning = false;

                await DisplayAlert("Information", "Votre message a été envoyé avec succès. Merci de votre confiance", "Ok");
                await Shell.Current.GoToAsync("..");
            }
            
            
        }
    }
}