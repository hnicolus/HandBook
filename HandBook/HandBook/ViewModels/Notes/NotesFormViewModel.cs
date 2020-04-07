using HandBook.Core.Functions;
using HandBook.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HandBook.ViewModels.Notes
{
    public class NotesFormViewModel :BaseViewModel
    {
        #region Fields
        bool _editing;
        Page page = Application.Current.MainPage;
        Note _note;
        #endregion

        #region Properties
        public string Title { get => _note.Title ;
            set
            {
                if (_note.Title == value)
                    return;
                _note.Title = value;
                NotifyPropertyChanged();
            }
        }

        public string Body
        {
            get => _note.Body;
            set
            {
                if (_note.Body == value)
                    return;
                _note.Body = value;
                NotifyPropertyChanged();
            }
        }
        public Note Note { get => _note;
            set
            {
                if (_note == value)
                    return;
                _note = value;
            }
        }
        #endregion

        #region Commands
        public Command SaveCommand { get; set; }
        #endregion

        #region Constructors
        public NotesFormViewModel()
        {
            _note = new Note();
            _editing = false;
            SaveCommand = new Command(OnSaveButtonClick);
        }
        public NotesFormViewModel(int id)
        {
            _editing = true;
            _note = DataAccess.GetNoteById(id);
            SaveCommand = new Command(OnSaveButtonClick);

        }

        #endregion

        #region Methods
        async void  OnSaveButtonClick()
        {
      
            //Save 
            if (_note.Id> 0)
            {
                var isUpdated = DataAccess.Update(_note);

                if (isUpdated)
                {
                    await page.DisplayAlert("Success", "Notes have been successfully Updated", "Ok");
                }
                else
                    await page.DisplayAlert("Failed", "Notes Failed to be Updated", "Ok");
            }
            else
            {
                
                var notesList = DataAccess.LoadNotes();
                if (notesList.Contains(_note))
                {
                    var isUpdated = DataAccess.Update(_note);
                    if (isUpdated)
                        await page.DisplayAlert("Success", "Notes have been successfully Updated", "Ok");
                    else
                        await page.DisplayAlert("Failed", "Notes Failed to be Updated", "Ok");
                }
                else
                {
                    var saved = DataAccess.Save(_note);
                    if (saved)
                    {
                        await page.DisplayAlert("Success", "Notes have been successfully Saved", "Ok");

                    }
                    else
                        await page.DisplayAlert("Failed", "Notes Failed to be save", "Ok");
                }

            }
        }
        #endregion
    }
}
