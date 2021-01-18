
using LookaukwatApp.Models.MobileModels;
using LookaukwatApp.ViewModels.Home;
using Xamarin.Forms;


namespace LookaukwatApp.Views
{
    
    public partial class ItemsPage : ContentPage
    {
       // ItemsViewModel _viewModel = new ItemsViewModel();
        string sort = "HeigherPrice";
        bool isSort = false;

        public string Sortby
        {
            get { return sort; }
            set { sort = value; }
        }

        public bool IsSort
        {
            get { return isSort; }
            set { isSort = value; }
        }
        public ItemsPage()
        {
            InitializeComponent();
           
        }

        //public ItemsPage(string sort , bool isSort)
        //{
        //    InitializeComponent();
        //    Sortby = sort;
        //    IsSort = isSort;
        //    _viewModel.OnApearing(Sortby, IsSort);
        //}
        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    // _viewModel.OnApearing(Sortby, true);
        //    mylist.ItemsSource = null;
        //}

    }
}