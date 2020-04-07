using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Ads;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HandBook.Core;
using HandBook.Droid.CustomRenders.Admob;
using HandBook.Views.Admob;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;


[assembly: ExportRenderer(typeof(AdmobView), typeof(AdMobViewRenderer))]
namespace HandBook.Droid.CustomRenders.Admob
{
    public class AdMobViewRenderer : ViewRenderer<AdmobView, AdView>
    {
        public AdMobViewRenderer(Context context) : base(context) { }

        private AdView CreateAdView()
        {

            var adView = new AdView(Context)
            {
                AdSize = AdSize.Banner,
                AdUnitId = Constants.BannerId,
                LayoutParameters = new LinearLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent)
            };

            adView.LoadAd(new AdRequest.Builder().Build());

            return adView;
        }

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<AdmobView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null && Control == null)
            {
                SetNativeControl(CreateAdView());
            }
        }
    }
}