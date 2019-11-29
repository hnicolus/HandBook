using HandBook.Models;
using HandBook.ViewModels;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandBook.Core.Functions;
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
            Refresh();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Refresh();
        }


        //Fetch  all items with isdeleted flg set to false
        private void Refresh()
        {
            Content.FetchList();

             var notes = Content.Notes.Where(b => b.IsDeleted == false).ToList();
             
             btnTopAdd.IsVisible = Content.TableExists();
            notesList.ItemsSource = notes
                .Where(b => b.IsDeleted == false)
                .OrderByDescending(b => b.Id);
            
            
        }

        //Selected Item
        private void NotesList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Note selectedNote = notesList.SelectedItem as Note;

            if(selectedNote != null)
            {
                Navigation.PushAsync(new NoteDetailPage(selectedNote));
            };
        }

        //Delete an item
        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;

            var note = menuItem.CommandParameter as Note;

            var response = await DisplayAlert("Warning", "Are you sure you want to delete this item ?", "Yes", "No");
            if (response)
            {
               var notDeleted = DataAccess.Delete(note);
                if (notDeleted)
                {
                    await DisplayAlert("Success", "Notes have been successfully Deleted", "Ok");
                }
                else
                {
                    await DisplayAlert("Failed", "Notes have Failed to be Deleted", "Ok");
                };
               
            }
            Refresh();
        }

        private void NotesList_Refreshing(object sender, EventArgs e)
        {
            Refresh();
            notesList.EndRefresh(); 
        }

       private async void BtnNewNote_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewNotesPage());
        }
    }
}