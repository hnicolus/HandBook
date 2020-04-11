using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace HandBook.Core.Functions
{
    public  class ShareExt
    {
        //Share text
        public async Task ShareText(string text)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = text,
                Title = "Share Text"
            });
        }


        //Share Links
        public async Task ShareUri(string uri)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Uri = uri,
                Title = "Share Web Link"
            });
        }
        //Share App Link
        public async Task ShareAppAsync()
        {
            var sharer = new ShareExt();
            await sharer.ShareUri("http://play.google.com/store/apps/details?id=" + AppInfo.PackageName.ToString());

        }

        //Get More Of My Products
        public async Task GetMoreAppsAsync()
        {
            var sharer = new ShareExt();
            await sharer.ShareUri("http://play.google.com/store/apps/dev?id=5801672477065122299");


        }
    }
}
