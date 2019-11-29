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
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Android.Gms.Ads.MobileAds.Initialize(ApplicationContext, "ca-app-pub-5780847061911787/9892180588");
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            string  dbName= "handBook_db.sqlite";
            string folderfPath =System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string FullPath = Path.Combine(folderfPath, dbName);
            LoadApplication(new App(FullPath));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}