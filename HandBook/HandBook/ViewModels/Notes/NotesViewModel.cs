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

namespace HandBook.ViewModels
{
    class NotesViewModel : BaseViewModel
    {
        #region Fileds
        Page page = App.Current.MainPage;
        private List<Note> notes;
        #endregion

        #region Properties
        public Note Note { get; set; }
        public List<Note> Notes { get; set; }
        #endregion

        #region Commands
        public Command DeleteCommand { get; set; }
        #endregion

        #region Constructors

        public NotesViewModel()
        {
            notes = new List<Note>();

            DeleteCommand = new Command(OnDeleteButtonClicked);
            FetchList();
        }

        private async void OnDeleteButtonClicked(object obj)
        {
            var menuItem = obj as MenuItem;

            var note = menuItem.CommandParameter as Note;

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

            }
            FetchList();
        }

        public NotesViewModel(int id)
        {
            FetchList();
            Note = notes.SingleOrDefault(n => n.Id == id);
        }
        #endregion




        //fetch Data from database
        public List<Note> FetchList()=> Notes =  DataAccess
            .LoadNotes()
            .Where(b => b.IsDeleted == false)
            .ToList();
        
        public bool TableExists
        { 
            get 
            {
                return (Notes.Count <= 0);
            } 
        }

  
    }
}
