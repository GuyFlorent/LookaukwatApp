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

        public SortItemsPage(string sortBy, string FromPage)
        {
            InitializeComponent();

            if (FromPage == "itemsPage")
            {
                SortSearchPage.IsVisible = false;
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
            else
            {
                SortItemHomePage.IsVisible = false;
                switch (sortBy)
                {
                    case "MostRecent":
                        mostRecentSearch.IsVisible = true;
                        mostOldSearch.IsVisible = false;
                        LowerPriceSearch.IsVisible = false;
                        HeigherPriceSearch.IsVisible = false;
                        break;

                    case "MostOld":
                        mostRecentSearch.IsVisible = false;
                        mostOldSearch.IsVisible = true;
                        LowerPriceSearch.IsVisible = false;
                        HeigherPriceSearch.IsVisible = false;
                        break;

                    case "LowerPrice":
                        mostRecentSearch.IsVisible = false;
                        mostOldSearch.IsVisible = false;
                        LowerPriceSearch.IsVisible = true;
                        HeigherPriceSearch.IsVisible = false;
                        break;

                    case "HeigherPrice":
                        mostRecentSearch.IsVisible = false;
                        mostOldSearch.IsVisible = false;
                        LowerPriceSearch.IsVisible = false;
                        HeigherPriceSearch.IsVisible = true;
                        break;
                    default:
                        mostRecentSearch.IsVisible = true;
                        mostOldSearch.IsVisible = false;
                        LowerPriceSearch.IsVisible = false;
                        HeigherPriceSearch.IsVisible = false;
                        break;
                }
            }
        }


        //private async void LowerPrice_Button(object o, EventArgs e)
        //{
        //    await PopupNavigation.Instance.PopAllAsync();
        //    await Navigation.PushAsync(new ItemsPage("MostOld",true));
        //}
    }
}