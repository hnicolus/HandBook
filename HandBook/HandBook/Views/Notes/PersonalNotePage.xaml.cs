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
        private NotesViewModel ctx ;
        public PersonalNotePage()
        {
            
            InitializeComponent ();

            Refresh();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Refresh();
        }


        //Fetch  all items with is deleted flg set to false
        private void Refresh()
        {
            ctx = null;
            ctx = new NotesViewModel(new PageService());
            BindingContext = ctx;  
        }

        //Selected Item
        private void NotesList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            notesList.SelectedItem = null;
        }

        //Delete an item
        private async  void BtnDelete_Clicked(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;

            var note = menuItem.CommandParameter as Note;
            await (BindingContext as NotesViewModel).OnDeleteButtonClicked(note);
            Refresh();
        }

        private void NotesList_Refreshing(object sender, EventArgs e)
        {
            Refresh();
            notesList.EndRefresh(); 
        }
        private void CreateNewNote(object sender, ItemTappedEventArgs e)
        {
            (BindingContext as NotesViewModel).AddCommand.Execute(sender);
        }
        private void notesList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Note selectedNote = e.Item as Note;
            (BindingContext as NotesViewModel).TappedItem(selectedNote);


        }
    }
}