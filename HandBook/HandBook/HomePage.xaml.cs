using HandBook.Views.RecycleBin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HandBook
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : TabbedPage
	{
		public HomePage ()
		{
			InitializeComponent ();
		}

        private async void BtnBin_Clicked(object sender, EventArgs e)
        {
           
            await Navigation.PushAsync(new RecycleHomePage());
           
        }
    }
}