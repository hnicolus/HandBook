using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HandBook.Views.Settings
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{
		public ObservableCollection<string> Items { get; set; }

		public SettingsPage()
		{
			InitializeComponent();

			Items = new ObservableCollection<string>
			{
				"Appearence",
				"Rate this app",
				"Backup & Restore Database",
				"More Apps",
				"Donate",
				"Contact us",
				"Terms and Conditions"
			};
			
			MyListView.ItemsSource = Items;
		}

		async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			if (e.Item == null)
				return;

			await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

			//Deselect Item
			((ListView)sender).SelectedItem = null;
		}
	}
}
