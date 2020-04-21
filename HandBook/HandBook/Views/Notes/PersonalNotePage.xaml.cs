using HandBook.Models;
using HandBook.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HandBook
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PersonalNotePage : ContentPage
    {
        #region Fields
        private NotesViewModel ctx ;
        #endregion

        #region Constructors
        public PersonalNotePage()
        {
            InitializeComponent ();
            Refresh();
        }
        #endregion

        #region Methods
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Refresh();
        }
        //Relaod items
        private void Refresh()
        {
            ctx = null;
            ctx = new NotesViewModel(new PageService());
            BindingContext = ctx;  
        }

        //Selected an item
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

            (BindingContext as NotesViewModel).TappedItemCommand.Execute(selectedNote as object);
        }

        private void ShareActionContext_Clicked(object sender, EventArgs e)
        {
            var item = sender as MenuItem;
            var selectedNote = item.CommandParameter as Note;

            (BindingContext as NotesViewModel).ShareItemCommand.Execute(selectedNote as object);
        }
        #endregion
    }
}