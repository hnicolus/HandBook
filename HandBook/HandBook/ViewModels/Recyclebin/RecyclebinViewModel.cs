using HandBook.Core.Functions;
using HandBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public Command ItemCommand { get; set; }
        public Command DeleteCommand { get; set; }
        #endregion

        #region Constructors
        public RecyclebinViewModel()
        {
            ItemCommand = new Command(OnItemSelected);
            DeleteCommand = new Command(OnDeleteButtonClicked);
            LoadNotes();
        }

        private void OnItemSelected(object obj)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Methods

       public void LoadNotes()
        {
            Notes = DataAccess.LoadNotes().Where(b =>b.IsDeleted == true).ToList();
        }

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
            //try
            //{
            //    var item = obj as Note;
            //    if (item is Note)
            //    {
            //       var isDeleted= DataAccess.Delete(item);
            //        if (isDeleted)
            //        {
            //           await app.DisplayAlert("Success", "Item Deleted", "Ok");
            //        }
            //        return;
            //    }
            //    else
            //    {
            //       await app.DisplayAlert("Failed", "Falied to delete item", "Ok");
            //    }
            //}catch(Exception ex)
            //{
            //   await app.DisplayAlert("Error", $"Report -{ex.Message}", "Ok");
            //}
            LoadNotes();
        }
        #endregion
    }
}
