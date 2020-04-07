
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace HandBook
{
    public partial class App : Application
    {
        public static string BaseImageUrl { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";

        public static string DatabaseLocation = string.Empty;

        public App()
        {

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjIzMDMxQDMxMzcyZTM0MmUzMG5jQTZrSE1PVlNWNGlaY0xmMWFKUHJyVlN5M0JrSE1vRkordHhOUHpkOGs9");
            InitializeComponent();

            MainPage = new NavigationPage(new HomePage());
            
        }
        public App(string databaseLocation)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjIzMDMxQDMxMzcyZTM0MmUzMG5jQTZrSE1PVlNWNGlaY0xmMWFKUHJyVlN5M0JrSE1vRkordHhOUHpkOGs9");

            DatabaseLocation = databaseLocation;
            InitializeComponent();
            MainPage = new NavigationPage(new HomePage());
        }
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
