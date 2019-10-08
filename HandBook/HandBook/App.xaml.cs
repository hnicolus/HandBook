using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace HandBook
{
    public partial class App : Application
    {

        public static string DatabaseLocation = string.Empty;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new HomePage());
            
        }
        public App(string databaselocation)
        {
            DatabaseLocation = databaselocation;
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
