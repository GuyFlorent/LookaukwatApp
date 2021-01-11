using LookaukwatApp.ViewModels;
using LookaukwatApp.Views;
using LookaukwatApp.Views.AppartmentView;
using LookaukwatApp.Views.HomeView;
using LookaukwatApp.Views.HouseView;
using LookaukwatApp.Views.ImageView;
using LookaukwatApp.Views.JobView;
using LookaukwatApp.Views.LoginView;
using LookaukwatApp.Views.ModeView;
using LookaukwatApp.Views.MultimediaView;
using LookaukwatApp.Views.PublishView;
using LookaukwatApp.Views.RegisterView;
using LookaukwatApp.Views.UserView;
using LookaukwatApp.Views.Vehicule;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace LookaukwatApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(JobDetailPage), typeof(JobDetailPage));
           // Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(Views.LoginView.LoginPage), typeof(Views.LoginView.LoginPage));
           // Routing.RegisterRoute(nameof(PublishProductPage), typeof(PublishProductPage));
            Routing.RegisterRoute(nameof(PublishAnnouncePage), typeof(PublishAnnouncePage));
            //Routing.RegisterRoute(nameof(UserProfilePage), typeof(UserProfilePage));
            Routing.RegisterRoute(nameof(UserPage), typeof(UserPage));
            Routing.RegisterRoute(nameof(Views.HomeView.ItemsPage), typeof(Views.HomeView.ItemsPage));
            Routing.RegisterRoute(nameof(JobAddPage), typeof(JobAddPage));
            Routing.RegisterRoute(nameof(LoginRedirectUserAccountPage), typeof(LoginRedirectUserAccountPage));
            Routing.RegisterRoute(nameof(RegisterRedirectLoginUserAccountPage), typeof(RegisterRedirectLoginUserAccountPage));
            Routing.RegisterRoute(nameof(ModeDetailPage), typeof(ModeDetailPage));
            Routing.RegisterRoute(nameof(ApartDetailPage), typeof(ApartDetailPage));
            Routing.RegisterRoute(nameof(MultimediaDetailPage), typeof(MultimediaDetailPage));
            Routing.RegisterRoute(nameof(VehiculeDetailPage), typeof(VehiculeDetailPage));
            Routing.RegisterRoute(nameof(HouseDetailPage), typeof(HouseDetailPage));
            Routing.RegisterRoute(nameof(UploadImagePage), typeof(UploadImagePage));
            Routing.RegisterRoute(nameof(ApartAddFirstPage), typeof(ApartAddFirstPage));
            Routing.RegisterRoute(nameof(ApartAddPage), typeof(ApartAddPage));
            Routing.RegisterRoute(nameof(ModeAddFirstPage), typeof(ModeAddFirstPage));
            Routing.RegisterRoute(nameof(ModeAddPage), typeof(ModeAddPage));
            Routing.RegisterRoute(nameof(MultimediaAddFirstPage), typeof(MultimediaAddFirstPage));
            Routing.RegisterRoute(nameof(MultimediaAddPage), typeof(MultimediaAddPage));
            Routing.RegisterRoute(nameof(HouseAddFirstPage), typeof(HouseAddFirstPage));
            Routing.RegisterRoute(nameof(HouseAddPage), typeof(HouseAddPage));
            Routing.RegisterRoute(nameof(VehiculeAddPage), typeof(VehiculeAddPage));
            Routing.RegisterRoute(nameof(VehiculeAddFirstPage), typeof(VehiculeAddFirstPage));
            Routing.RegisterRoute(nameof(UpdateUserPasswordPage), typeof(UpdateUserPasswordPage));
           // Routing.RegisterRoute(nameof(UpdateUserInfoPage), typeof(UpdateUserInfoPage));
            Routing.RegisterRoute(nameof(UserAnnoucePage), typeof(UserAnnoucePage));
            Routing.RegisterRoute(nameof(UserPersonalInfoPage), typeof(UserPersonalInfoPage));
            Routing.RegisterRoute(nameof(UserUpdateInfoPage), typeof(UserUpdateInfoPage));
            //Routing.RegisterRoute(nameof(SearchPage), typeof(SearchPage));
            //Routing.RegisterRoute(nameof(ResultSearchPage), typeof(ResultSearchPage));
        }

    }
}
