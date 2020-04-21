using HandBook.Core.Functions;
using HandBook.Models;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace HandBook.ViewModels.Lyrics
{
    public class LyricsFormViewModel:BaseViewModel
    {

        #region Fields
        private Lyric _lyric;
        private readonly IPageService _page;
        #endregion

        #region Properties
        public Lyric Lyric 
        {
            get => _lyric;
            set
            {
                if (_lyric == value)
                    return;
                _lyric = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Commands
        public ICommand DeletCommand { get; set; }
        public ICommand EditorSettingsCommand { get; set; }

        public ICommand BackButtonCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        #endregion

        #region Constructors
        public LyricsFormViewModel(IPageService page)
        {
            Lyric = new Lyric();
            InstantiateCommands();
            _page = page;
        }

        public LyricsFormViewModel(int id, IPageService page)
        {
            InstantiateCommands();
            Lyric = DataAccess.GetLyricById(id);
            _page = page;
        }
        #endregion

        #region Methods

        void InstantiateCommands()
        {
            EditorSettingsCommand = new Command(OnEditorButtonClicked);
            BackButtonCommand = new Command(OnBackButtonClicked);
            DeletCommand = new Command(DeleteLyric);
            SaveCommand = new Command(SaveLyric);
        }

        private void OnEditorButtonClicked(object obj)
        {
            throw new NotImplementedException();
        }

        private void OnBackButtonClicked(object obj) => Application.Current.MainPage.Navigation.PopAsync();
        private async void SaveLyric(object obj)
        {
    
            //Save 
            if(string.IsNullOrEmpty(Lyric.Title))
            {
               await _page.DisplayAlert("Error", "Title cannot be empty", "Ok");
            }
            else
            {
                if (Lyric.Id > 0)
                {
                    var isUpdated = DataAccess.Update(Lyric);

                    if (isUpdated)
                    {
                        await _page.DisplayAlert("Success", "Notes have been successfully Updated", "Ok");
                    }
                    else
                        await _page.DisplayAlert("Failed", "Notes Failed to be Updated", "Ok");
                }
                else
                {

                    var notesList = DataAccess.LoadLyrics();
                    if (notesList.Contains(Lyric))
                    {
                        var isUpdated = DataAccess.Update(Lyric);
                        if (isUpdated)
                            await _page.DisplayAlert("Success", "Notes have been successfully Updated", "Ok");
                        else
                            await _page.DisplayAlert("Failed", "Notes Failed to be Updated", "Ok");
                    }
                    else
                    {
                        var saved = await DataAccess.SaveAsync(Lyric);
                        if (saved)
                        {
                            await _page.DisplayAlert("Success", "Notes have been successfully Saved", "Ok");

                        }
                        else
                            await _page.DisplayAlert("Failed", "Notes Failed to be save", "Ok");
                    }
                }
            }
       
        }
        private async void DeleteLyric(object obj)
        {
            var response = await _page.DisplayAlert("Warning", "Are you sure you want to Delete this?", "Yes", "No");

            if (response)
            {
                 Lyric.IsDeleted = true;
                var deleted = Lyric.IsDeleted;
                DataAccess.Update(Lyric);

                if (deleted)
                {
                    await _page.DisplayAlert("Success", "Lyrics have been successfully Deleted", "Ok");
                }
                else
                {
                    await _page.DisplayAlert("Failed", "Lyrics have Failed to be Deleted", "Ok");
                };

            }
            else
            {
                var continueEdit = await _page.DisplayAlert("Notice", "Do you want to Continue Editing ", "Yes", "No");
                if (continueEdit != true)
                {
                    await _page.PopAsync();
                }
            }
        }
        #endregion
    }
}
