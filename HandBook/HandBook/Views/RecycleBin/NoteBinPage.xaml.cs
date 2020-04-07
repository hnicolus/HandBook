using HandBook.Models;
using HandBook.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandBook.Core.Functions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HandBook.ViewModels.Recyclebin;

namespace HandBook.Views.RecycleBin
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NoteBinPage : ContentPage
	{
		private RecyclebinViewModel ctx;
		public NoteBinPage()
		{

			InitializeComponent();

			Refresh();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			Refresh();
		}

		private void Refresh()
		{
			ctx = new RecyclebinViewModel();
			BindingContext = ctx;
		}

		private async void NotesList_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			var response = await DisplayAlert("Alert", "Do you want to Restore Selected File ?", "Yes", "No");
			if (!response) return;
			if (NotesList.SelectedItem is Note note)
			{
				note.IsDeleted = false;
				DataAccess.Update(note);
				Refresh();
			}


		}

		private async void btnDelete_Clicked(object sender, EventArgs e)
		{
			var app = App.Current.MainPage;

			if (sender is MenuItem menuItem)
			{
				var note = menuItem.CommandParameter as Note;
				var response = await app.DisplayAlert("Warning", "Are you sure you want to delete this item ?", "Yes", "No");
				if (response)
				{
					if (note != null)
					{
						note.IsDeleted = true;

						var delete = DataAccess.Delete(note);

						if (delete)
						{
							await app.DisplayAlert("Success", "Notes have been successfully Deleted", "Ok");
						}
						else
						{
							await app.DisplayAlert("Failed", "Notes have Failed to be Deleted", "Ok");
						}
					}

				}
				Refresh();
			}
		}
	}
}