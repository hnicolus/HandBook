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

		private new NotesViewModel Content = new NotesViewModel();
		public NoteBinPage ()
		{
			InitializeComponent ();
			refresh();
		}

		async private void BtnDelete_Clicked(object sender, EventArgs e)
		{
			var menuItem = sender as MenuItem;
			var note = menuItem.CommandParameter as Note;
			var response = await DisplayAlert("Warning", "Are you sure you want to delete this item ?", "Yes", "No");
			if (response)
			{
				note.IsDeleted = true;

				var Delete = Crud.DeleteItem(note);

				if (Delete)
				{
					await DisplayAlert("Success", "Notes have been successfully Deleted", "Ok");
				}
				else
				{
					await DisplayAlert("Failed", "Notes have Failed to be Deleted", "Ok");
				};

			}
			refresh();

		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			refresh();
		}

		private void refresh()
		{
			var list = Content.Notes;
			list = list.Where(b => b.IsDeleted).OrderBy(b => b.Id).ToList();
			NotesList.ItemsSource = list;
		}

		private async void NotesList_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			var note = NotesList.SelectedItem as Note;
			
			var response = await DisplayAlert("Alert", "Do you want to Restore Selected File ?", "Yes", "No");
			if (response)
			{
				note.IsDeleted = false;
			}
		}
	}
}