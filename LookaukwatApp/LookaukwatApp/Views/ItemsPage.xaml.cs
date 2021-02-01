
using LookaukwatApp.Models.MobileModels;
using LookaukwatApp.ViewModels.Home;
using LookaukwatApp.ViewModels.OtherServices;
using System;
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
            
           // Shell.SetNavBarIsVisible(this, false);
        }

        
        //private void Favorite_Click(object sender, EventArgs e)
        //{

        //    ImageButton button = (ImageButton)sender;
        //    StackLayout listViewItem = (StackLayout)button.Parent;
        //    //bool response = CheckFavorite.IsFabvorite(item);
        //    //if (response)
        //    //{
        //    //    button.Source = "heart_red";
        //    //}
        //    //else
        //    //{
        //    //    button.Source = "heart_black";
        //    //}
        //    button.Source = "heart_red";
        //}



        int lastItemIndex;
        int currentItemIndex;

        void ListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            //MyItemType item = e.Item as MyItemType;

            //currentItemIndex = Items.IndexOf(item);
            currentItemIndex = e.ItemIndex;
            if (currentItemIndex > lastItemIndex)
            {
                Img.IsVisible = false;
               
            }
            else
            {
                Img.IsVisible = true;
                
            }
            lastItemIndex = currentItemIndex;
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