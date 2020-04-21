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
        private readonly IPageService _pageService;
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
        public NotesFormViewModel(IPageService pageService)
        {
            _note = new Note();

            InitialiseCommands();
            _pageService = pageService;
        }
        public NotesFormViewModel(IPageService pageService, int id)
        {
            _note = DataAccess.GetNoteById(id);
            InitialiseCommands();
            _pageService = pageService;
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
            bool response = false;
            if(string.IsNullOrEmpty(Note.Title) || string.IsNullOrEmpty(Note.Body))
            {
                 response = await page.DisplayAlert("Warning", "Are you sure you want to discard and exit the note creator", "Yes", "No");
            }
            else
            {
                response = await page.DisplayAlert("Warning", "Are you sure you want to Recycle this Note", "Yes", "No");
            }
            if (response)
            {
               
                var note = DataAccess.GetNoteById(item.Id);
                if (note != null)
                {
                    item.IsDeleted = true;
                    DataAccess.Update(item);
                    await _pageService.PopAsync();
                    await _pageService.PopAsync();
                }
                else
                {
                    await _pageService.PopAsync();
                };
            }
        }

        private void OnEditorSettingsClicked(object obj)
        {
            throw new NotImplementedException();
        }

        private async void OnBackButtonClicked(object obj)
        {
            await _pageService.PopAsync();
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
            if(_note.Title == null)
            {
               await _pageService.DisplayAlert("Error", "Title cannot be left empty!", "Ok");
            }
            else
            {
                if (_note.Id > 0)
                {
                    var isUpdated = DataAccess.Update(_note);

                    if (isUpdated)
                    {
                        await _pageService.DisplayAlert("Success", "Notes have been successfully Updated", "Ok");
                    }
                    else
                        await _pageService.DisplayAlert("Failed", "Notes Failed to be Updated", "Ok");
                }
                else
                {
                    var notes = DataAccess.LoadNotes();
                    if (notes.Contains(_note))
                    {
                        var isUpdated = DataAccess.Update(_note);
                        if (isUpdated)
                            await _pageService.DisplayAlert("Success", "Notes have been successfully Updated", "Ok");
                        else
                            await _pageService.DisplayAlert("Failed", "Notes Failed to be Updated", "Ok");
                    }
                    else
                    {
                        Random rnd = new Random();
                        var startGradient = Color.FromRgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
                        var EndGradient = Color.FromRgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));

                        _note.BackgroundGradientStart = startGradient.ToHex();
                        _note.BackgroundGradientEnd = EndGradient.ToHex();
                        var saved = DataAccess.Save(_note);
                        if (saved)
                        {
                            await _pageService.DisplayAlert("Success", "Notes have been successfully Saved", "Ok");

                        }
                        else
                            await _pageService.DisplayAlert("Failed", "Notes Failed to be save", "Ok");
                    }

                }
            }

        }
        #endregion
    }
}
