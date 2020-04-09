
using HandBook.Views.Home;
using HandBook.Views.Notes;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace HandBook
{
    public partial class App : Application
    {
        public static string BaseImageUrl { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";
        private const string SYNCFUSION_LICENSE = "MjIzMDMxQDMxMzcyZTM0MmUzMG5jQTZrSE1PVlNWNGlaY0xmMWFKUHJyVlN5M0JrSE1vRkordHhOUHpkOGs9";
        public static string DatabaseLocation = string.Empty;


        #region PropertyKeys
        private const string editorBackgroundColorKey = "EditorBackgroundColor";
        #endregion
        public Color EditorBackgroundColor
        {
            get
            {
                if (Properties.ContainsKey(editorBackgroundColorKey))
                    return (Color) Properties[editorBackgroundColorKey];
                return Color.WhiteSmoke;
            }
            set
            {
                Properties[editorBackgroundColorKey] = value;
            }
        }
        public App()
        {
            ActivateSyncFusion();
           InitializeComponent();
            MainPage = new NavigationPage(new BottomNavigationPage())
            {
                BackgroundColor = Color.WhiteSmoke,
                BarBackgroundColor = Color.FromHex("#ffffff"),
                BarTextColor = Color.Black,

            };

        }
        public App(string databaseLocation)
        {
            ActivateSyncFusion();
            DatabaseLocation = databaseLocation;
            InitializeComponent();
            MainPage = new NavigationPage(new BottomNavigationPage()) 
            {
                BackgroundColor = Color.WhiteSmoke,
                BarBackgroundColor = Color.FromHex("#ff4a4a"),
                BarTextColor = Color.Black,

            };
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
        void ActivateSyncFusion() => Syncfusion.Licensing.SyncfusionLicenseProvider
            .RegisterLicense(SYNCFUSION_LICENSE);


    }
}
