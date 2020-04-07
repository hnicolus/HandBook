using Syncfusion.SfRotator.XForms.iOS;
using Syncfusion.XForms.iOS.Core;
using Syncfusion.XForms.iOS.Buttons;
using Syncfusion.XForms.iOS.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Foundation;
using UIKit;

namespace HandBook.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            //Google.MobileAds.MobileAds.Configure("ca-app-pub-1234567891234567~1234567890");
            // Syncfusion.SfSchedule.XForms.iOS.SfScheduleRenderer.Init();
            //Syncfusion.XForms.iOS.RichTextEditor.SfRichTextEditorRenderer.Init();
            global::Xamarin.Forms.Forms.Init();
            SfRotatorRenderer.Init();
            SfButtonRenderer.Init();
            SfGradientViewRenderer.Init();
           
            string dbName = "handBook_db.sqlite";
            string folderfPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),"..","Library");
            string fullPath = Path.Combine(folderfPath, dbName);

            LoadApplication(new App(fullPath));

            return base.FinishedLaunching(app, options);
        }
    }
}
