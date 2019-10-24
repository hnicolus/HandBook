using HandBook.Models;
using HandBook.ViewModels;
using SQLite;
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
	public partial class PersonalNotePage : ContentPage
	{
		private new NotesViewModel Content;
		public PersonalNotePage()
		{
			InitializeComponent ();
			Content = new NotesViewModel();
			refresh();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			refresh();
		}

		private void refresh()
		{
			 var notes = Content.refresh();
			btnTopAdd.IsVisible = Content.tableExists;
			notesList.ItemsSource = notes.Where(B => B.IsDeleted == false).OrderByDescending(b => b.Id);  
		}
		private void NotesList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			Note selectedNote = notesList.SelectedItem as Note;

			if(selectedNote != null)
			{
				Navigation.PushAsync(new NoteDetailPage(selectedNote));
			};
		}


 

		async private void BtnDelete_Clicked(object sender, EventArgs e)
		{
			var menuItem = sender as MenuItem;

			var note = menuItem.CommandParameter as Note;

			var response = await DisplayAlert("Warning", "Are you sure you want to delete this item ?", "Yes", "No");
			if (response)
			{
				note.IsDeleted = true;
				
				if (note.IsDeleted)
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

		private void NotesList_Refreshing(object sender, EventArgs e)
		{
			refresh();
			notesList.EndRefresh(); 
		}

	   async private void BtnNewNote_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new NewNotesPage());
		}
	}
}