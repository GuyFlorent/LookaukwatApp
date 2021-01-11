using LookaukwatApp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace LookaukwatApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}