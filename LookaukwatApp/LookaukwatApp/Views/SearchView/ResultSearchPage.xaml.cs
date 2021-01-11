using LookaukwatApp.ViewModels.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LookaukwatApp.Views.SearchView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultSearchPage : ContentPage
    {
        public ResultSearchPage()
        {
            InitializeComponent();
            BindingContext = new ResultSearchViewModel();
        }
    }
}