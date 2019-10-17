using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace HandBook
{
    public partial class App : Application
    {

        public static string DatabaseLocation = string.Empty;
        
        private string password,nickName,favouriteColor,cityBorn;
   
        public string Password
        {
            get
            {
                if (Properties.ContainsKey(password))
                {
                    return Properties[password].ToString();
                }
                else
                    return "";
            }
            set
            {
                Properties[password] = value;
            }
        }

        public string NickName
        {
            get
            {
                if (Properties.ContainsKey(nickName))
                {
                    return Properties[nickName].ToString();
                }
                else
                    return "";
            }
            set
            {
                Properties[nickName] = value;
            }
        }

        public string FavouriteColor
        {
            get
            {
                if (Properties.ContainsKey(favouriteColor))
                {
                    return Properties[favouriteColor].ToString();
                }
                else
                    return "";
            }
            set
            {
                Properties[favouriteColor] = value;
            }
        }

        public string CityBorn
        {
            get
            {
                if (Properties.ContainsKey(cityBorn))
                {
                    return Properties[cityBorn].ToString();
                }
                else
                    return "";
            }
            set
            {
                Properties[cityBorn] = value;
            }
        }

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
