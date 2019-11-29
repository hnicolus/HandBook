using HandBook.Views.RecycleBin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandBook.Views.Settings;
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

		private async void btnAbout_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new AboutPage());
		}

		private async void Settings_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new SettingsPage()); ;
		}
	}
}