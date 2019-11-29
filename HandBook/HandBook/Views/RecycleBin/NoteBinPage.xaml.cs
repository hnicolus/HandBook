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

namespace HandBook.Views.RecycleBin
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NoteBinPage : ContentPage
	{

		private readonly NotesViewModel _content = new NotesViewModel();
		public NoteBinPage ()
		{
			InitializeComponent ();
			Refresh();
		}

		async private void BtnDelete_Clicked(object sender, EventArgs e)
		{
			if (sender is MenuItem menuItem)
			{
				var note = menuItem.CommandParameter as Note;
				var response = await DisplayAlert("Warning", "Are you sure you want to delete this item ?", "Yes", "No");
				if (response)
				{
					if (note != null)
					{
						note.IsDeleted = true;

						var delete = DataAccess.Delete(note);

						if (delete)
						{
							await DisplayAlert("Success", "Notes have been successfully Deleted", "Ok");
						}
						else
						{
							await DisplayAlert("Failed", "Notes have Failed to be Deleted", "Ok");
						}
					}

				}
			}

			Refresh();

		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			Refresh();
		}

		private void Refresh()
		{
			var list = _content.Notes;
			list = list.Where(b => b.IsDeleted).OrderBy(b => b.Id).ToList();
			NotesList.ItemsSource = list;
		}

		private async void NotesList_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			var response = await DisplayAlert("Alert", "Do you want to Restore Selected File ?", "Yes", "No");
			if (!response) return;
			if (NotesList.SelectedItem is Note note) note.IsDeleted = false;
		}
	}
}