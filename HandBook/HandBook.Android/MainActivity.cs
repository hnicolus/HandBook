using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;

namespace HandBook.Droid
{
    [Activity(Label = "HandBook", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjIzMDMxQDMxMzcyZTM0MmUzMG5jQTZrSE1PVlNWNGlaY0xmMWFKUHJyVlN5M0JrSE1vRkordHhOUHpkOGs9");
  
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

     

            Android.Gms.Ads.MobileAds.Initialize(ApplicationContext, "ca-app-pub-5780847061911787~3737835778");

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);


            string  dbName= "handBook_db.sqlite";
            string folderfPath =System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string FullPath = Path.Combine(folderfPath, dbName);
            LoadApplication(new App(FullPath));
            Window.SetSoftInputMode(Android.Views.SoftInput.AdjustResize);

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}