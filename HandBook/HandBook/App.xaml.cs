
using HandBook.Core;
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

        #region Constants
        public static string BaseImageUrl { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";
        #endregion

        public static string DatabaseLocation = string.Empty;

        #region PropertyKeys
        private const string editorBackgroundColorKey = "EditorBackgroundColor";
        #endregion

        #region Properties
        public Color EditorBackgroundColor
        {
            get
            {
                if (Properties.ContainsKey(editorBackgroundColorKey))
                    return (Color)Properties[editorBackgroundColorKey];
                return Color.WhiteSmoke;
            }
            set
            {
                Properties[editorBackgroundColorKey] = value;
            }
        }
        #endregion


        #region Constructors
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
        #endregion


        #region Methods

        //Activate SF License                                  
        void ActivateSyncFusion() => Syncfusion.Licensing.SyncfusionLicenseProvider
                                    .RegisterLicense(Constants.SFLicenseKey);
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


        #endregion


    }
}
