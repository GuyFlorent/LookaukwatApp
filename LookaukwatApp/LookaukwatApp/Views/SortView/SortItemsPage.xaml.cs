using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LookaukwatApp.Views.SortView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SortItemsPage 
    {
        public SortItemsPage()
        {
            InitializeComponent();
            //this.BindingContext = new LookaukwatApp.ViewModels.SortBy.SortByViewModel();
        }

        public SortItemsPage(string sortBy)
        {
            InitializeComponent();
            switch (sortBy)
            {
                case "MostRecent":
                    mostRecent.IsVisible = true;
                    mostOld.IsVisible = false;
                    LowerPrice.IsVisible = false;
                    HeigherPrice.IsVisible = false;
                    break;

                case "MostOld":
                    mostRecent.IsVisible = false;
                    mostOld.IsVisible = true;
                    LowerPrice.IsVisible = false;
                    HeigherPrice.IsVisible = false;
                    break;

                case "LowerPrice":
                    mostRecent.IsVisible = false;
                    mostOld.IsVisible = false;
                    LowerPrice.IsVisible = true;
                    HeigherPrice.IsVisible = false;
                    break;

                case "HeigherPrice":
                    mostRecent.IsVisible = false;
                    mostOld.IsVisible = false;
                    LowerPrice.IsVisible = false;
                    HeigherPrice.IsVisible = true;
                    break;
                default:
                    mostRecent.IsVisible = true;
                    mostOld.IsVisible = false;
                    LowerPrice.IsVisible = false;
                    HeigherPrice.IsVisible = false;
                    break;
            }
        }

        //private async void LowerPrice_Button(object o, EventArgs e)
        //{
        //    await PopupNavigation.Instance.PopAllAsync();
        //    await Navigation.PushAsync(new ItemsPage("MostOld",true));
        //}
    }
}