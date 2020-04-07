using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using HandBook.iOS.CustomRenderers.Admob;
using Google.MobileAds;


using Foundation;
using UIKit;

[assembly: ExportRenderer(typeof(AdMobView), typeof(AdMobViewRenderer))]

namespace HandBook.iOS.CustomRenderers.Admob
{
    class AdMobViewRenderer : ViewRenderer<AdMobView, BannerView>
    {
        private Request GetAdRequest()
        {
            var request = Request.GetDefaultRequest();
            return request;
        }

        private BannerView CreateBannerView()
        {
            var bannerView = new BannerView(AdSizeCons.SmartBannerPortrait)
            {
                AdUnitID = "ca-app-pub-3940256099942544/6300978111",
                RootViewController = GetViewController()
            };

            bannerView.LoadRequest(GetAdRequest());

            return bannerView;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<AdMobView> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
            {
                SetNativeControl(CreateBannerView());
            }
        }

        private UIViewController GetViewController()
        {
            var windows = UIApplication.SharedApplication.Windows;
            return (from window in windows where window.RootViewController != null select window.RootViewController).FirstOrDefault();
        }
    }
}