using HandBook.Core.Functions;
using HandBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HandBook.ViewModels.Recyclebin
{
   public class RecyclebinViewModel:BaseViewModel
    {
        #region Fields
        private List<Note> notes;
        #endregion

        #region Properties
        public List<Note> Notes 
        { get => notes;
            set
            {
                if (notes == value)
                    return;
                notes = value;
                NotifyPropertyChanged();
            } 
        }

        #endregion

        #region Commands
        public Command ItemTappedCommand { get; set; }
        public Command DeleteAllCommand { get; set; }
        public Command BackButtonCommand { get; set; }
        public Command DeleteCommand { get; set; }
        #endregion

        #region Constructors
        public RecyclebinViewModel()
        {
            ItemTappedCommand = new Command(async n => await OnItemTapped(n as Note));
            DeleteCommand = new Command(OnDeleteButtonClicked);
            DeleteAllCommand = new Command(OnDeleteAllButtonClicked);
            BackButtonCommand = new Command(OnBackButtonClicked);
            LoadNotes();
        }

        #endregion

        #region Methods
        private async void OnBackButtonClicked(object obj) => await App.Current.MainPage.Navigation.PopAsync();
        private async void OnDeleteAllButtonClicked(object obj)
        {
            var page = App.Current.MainPage;
            var response = await page.DisplayAlert("Warning", "Clearing the Notes Recycle Bin will" +
                                                    "delete all the items " +
                                                    ",Are you sure you want to " +
                                                    "permanently delete all the recycled " +
                                                    "notes?", "Yes", "Cancel");
            if (response)
            {
                foreach (Note n in Notes)
                {
                    DataAccess.Delete(n);
                }
            }
            LoadNotes();
        }
        private async Task OnItemTapped(Note note)
        {
            var page = App.Current.MainPage;
            var response = await page.DisplayAlert("Alert", "Do you want to Restore Selected File ?", "Yes", "No");
            if (!response) return;
                note.IsDeleted = false;
                DataAccess.Update(note);
                LoadNotes();
        }

        public void LoadNotes()=> Notes = DataAccess.LoadNotes().Where(b => b.IsDeleted == true).ToList();
        async void OnDeleteButtonClicked(object obj)
        {
            var app = App.Current.MainPage;
            var sd = obj as Post;
            if (obj is MenuItem menuItem)
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
            }

            LoadNotes();
        }
        #endregion
    }
}
