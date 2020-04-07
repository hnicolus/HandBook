using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HandBook.Core
{
    public static class Constants
    {

        public static string AppId
        {
            get
            {
                switch (Device.RuntimePlatform)
                {
                    case Device.Android:
                        return "ca-app-pub-5780847061911787~3737835778";
                    default:
                        return "ca-app-pub-5780847061911787~3737835778";
                }
            }
        }
        public static string BannerId
        {

            get
            {
                switch (Device.RuntimePlatform)
                {
                    case Device.Android:
                        return "ca-app-pub-5780847061911787/9892180588";
                    case Device.iOS:
                        return "ca-app-pub-3940256099942544/6300978111";
                    default:
                        return "ca-app-pub-5780847061911787/9892180588";
                }
            }
        }

        public static string InterstitialAdId
        {
            get
            {
                switch (Device.RuntimePlatform)
                {
                    case Device.Android:
                        return "ca-app-pub-5780847061911787/5652254841";
                    default:
                        return "ca-app-pub-5780847061911787/5652254841";
                }
            }
        }

        public static bool ShowAds
        {
            get
            {
                _adCounter++;
                if (_adCounter % 5 == 0)
                {
                    return true;
                }
                return false;
            }
        }

        private static int _adCounter;


    }
}
