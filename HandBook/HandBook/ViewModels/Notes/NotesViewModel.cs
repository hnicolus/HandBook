using HandBook.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using HandBook.Core.Functions;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Windows.Input;
using System.Collections.ObjectModel;
using HandBook.Views.Notes;

namespace HandBook.ViewModels
{
    class NotesViewModel : BaseViewModel
    {
        #region Fields
        Page page = App.Current.MainPage;
        private List<Note> notes;
        private readonly IPageService pageService;
        #endregion

        #region Properties
        public Note SelectedNote { get; set; }
        public Note Note { get; set; }
        public List<Note> Notes 
        { 
            get =>notes;
            private set
            {
                if (notes == value)
                    return;
                notes = value;
                NotifyPropertyChanged();

            }
        }
        #endregion

        #region Commands
        public ICommand DeleteNoteCommand { get; private  set; }
        public Command AddCommand { get; set; }
        #endregion

        #region Constructors

        public NotesViewModel(IPageService pageService)
        {
            notes = new List<Note>();

            DeleteNoteCommand = new Command<Note>(async n => await OnDeleteButtonClicked(n as Note));
            AddCommand = new Command(AddNewNote);
            FetchList();
            this.pageService = pageService;
        }


        public NotesViewModel(int id)
        {
            FetchList();
            Note = notes.SingleOrDefault(n => n.Id == id);
        }
        #endregion


        #region Methods
        public void FetchList() =>
    Notes = DataAccess.LoadNotes().Where(b => b.IsDeleted == false).ToList();
        private async void AddNewNote(object obj)
        {
            await page.Navigation.PushAsync(new NotesFormPage());
        }

        public void TappedItem(Note note)
        {
            if (note == null)
                return;
            page.Navigation.PushAsync(new NoteDetailPage(note.Id));

        }
        public async Task OnDeleteButtonClicked(Note note)
        {

            var response = await page.DisplayAlert("Warning", "Are you sure you want to delete this item ?", "Yes", "No");
            if (response)
            {
                note.IsDeleted = true;

                DataAccess.Update(note);
                if (note.IsDeleted)
                {
                    await page.DisplayAlert("Success", "Notes have been successfully Deleted", "Ok");

                }
                else
                {
                    await page.DisplayAlert("Failed", "Notes have Failed to be Deleted", "Ok");
                };
                FetchList();
            }
        }

        public bool TableExists
        {
            get
            {
                return (Notes.Count <= 0);
            }
        }
        #endregion


    }
}
