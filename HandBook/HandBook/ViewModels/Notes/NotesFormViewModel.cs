using Amporis.Xamarin.Forms.ColorPicker;
using HandBook.Core.Functions;
using HandBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HandBook.ViewModels.Notes
{
    public class NotesFormViewModel :BaseViewModel
    {
        #region Fields
        Page page = Application.Current.MainPage;
        App app = Application.Current as App;
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
        public Color EditorBackgroundColor { get => app.EditorBackgroundColor;
            private set
            {
                if (app.EditorBackgroundColor == value)
                    return;
                app.EditorBackgroundColor = value;
                NotifyPropertyChanged();
            }
        }

        #region Commands
        public Command SaveCommand { get; set; }
        public Command DeleteCommand { get; set; }
        public Command BackButtonCommand { get; set; }
        public Command EditorSettingsCommand { get; set; }
        public Command BackgroundColorCommand { get; set; }
        #endregion

        #region Constructors
        public NotesFormViewModel()
        {
            _note = new Note();

            InitialiseCommands();
        }
        public NotesFormViewModel(int id)
        {
            _note = DataAccess.GetNoteById(id);
            InitialiseCommands();
        }

        #endregion

        #region Methods
        private void InitialiseCommands()
        {
            SaveCommand = new Command(OnSaveButtonClick);
            DeleteCommand = new Command(async it => await OnDeleteButtonClicked(it as Note));
            BackButtonCommand = new Command(OnBackButtonClicked);
            EditorSettingsCommand = new Command(OnEditorSettingsClicked);
            BackgroundColorCommand = new Command<StackLayout>(async lay => await SetBackgroundColor(lay));

        }
        private async Task OnDeleteButtonClicked(Note item)
        {
            var response = await page.DisplayAlert("Warning", "Are you sure you want to Recycle this Note", "Yes", "No");
            if (response)
            {
               
                var notes = DataAccess.LoadNotes();
                var note = notes.SingleOrDefault(b => b.Id == item.Id);
                if (note != null)
                {
                    item.IsDeleted = true;
                    DataAccess.Update(item);
                    await page.Navigation.PopAsync();
                    await page.Navigation.PopAsync();
                }
                else
                {
                    await page.Navigation.PopAsync();
                };
            }
        }

        private void OnEditorSettingsClicked(object obj)
        {
            throw new NotImplementedException();
        }

        private async void OnBackButtonClicked(object obj)
        {
            await page.Navigation.PopAsync();
        }

        public async Task SetBackgroundColor(StackLayout layout)
        {
            var app = Application.Current as App;

            EditorBackgroundColor = await ColorPickerDialog.Show(layout, "ColorPickerDialog", Color.White, null);
            await app.SavePropertiesAsync();
        }

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
                var notes = DataAccess.LoadNotes();
                if (notes.Contains(_note))
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
