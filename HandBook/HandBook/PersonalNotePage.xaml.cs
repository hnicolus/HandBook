using HandBook.Models;
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
		public PersonalNotePage()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            refresh();
        }

        private void refresh()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Note>();
                var notes = conn.Table<Note>().ToList();
                if(notes.Count <= 0)
                {
                    btnTopAdd.IsVisible = true;
                }
                else
                {
                    btnTopAdd.IsVisible = false;
                }
                notesList.ItemsSource = notes;

            }
        }
        private void NotesList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedNote = notesList.SelectedItem as Note;

            if(selectedNote != null)
            {
                Navigation.PushAsync(new NoteDetailPage(selectedNote));
            };
        }


 

        private void BtnDelete_Clicked(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;

            var note = menuItem.CommandParameter as Note;
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Note>();
                int rows = conn.Delete(note);

                if (rows > 0)
                {
                    DisplayAlert("Success", "Notes have been successfully Deleted", "Ok");
                }
                else
                {
                    DisplayAlert("Failed", "Notes have Failed to be Deleted", "Ok");
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