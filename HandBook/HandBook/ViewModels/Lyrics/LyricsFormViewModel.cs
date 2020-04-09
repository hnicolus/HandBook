using HandBook.Core.Functions;
using HandBook.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace HandBook.ViewModels.Lyrics
{
    public class LyricsFormViewModel:BaseViewModel
    {

        #region Fields
        private Lyric _lyric;
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
        public  ICommand SaveCommand { get; set; }

        #endregion

        #region Constructors
        public LyricsFormViewModel()
        {
            Lyric = new Lyric();
            InstantiateCommands();
        }

        public LyricsFormViewModel(int id)
        {
            InstantiateCommands();
            Lyric = DataAccess.GetLyricById(id);
        }
        #endregion

        #region Methods

        void InstantiateCommands()
        {
            DeletCommand = new Command(DeleteLyric);
            SaveCommand = new Command(SaveLyric);
        }

        private async void SaveLyric(object obj)
        {
            var page = App.Current.MainPage;
            //Save 
            if (Lyric.Id > 0)
            {
                var isUpdated = DataAccess.Update(Lyric);

                if (isUpdated)
                {
                    await App.Current.MainPage.DisplayAlert("Success", "Notes have been successfully Updated", "Ok");
                }
                else
                    await App.Current.MainPage.DisplayAlert("Failed", "Notes Failed to be Updated", "Ok");
            }
            else
            {

                var notesList = DataAccess.LoadLyrics();
                if (notesList.Contains(Lyric))
                {
                    var isUpdated = DataAccess.Update(Lyric);
                    if (isUpdated)
                        await page.DisplayAlert("Success", "Notes have been successfully Updated", "Ok");
                    else
                        await page.DisplayAlert("Failed", "Notes Failed to be Updated", "Ok");
                }
                else
                {
                    var saved = await DataAccess.SaveAsync(Lyric);
                    if (saved)
                    {
                        await page.DisplayAlert("Success", "Notes have been successfully Saved", "Ok");

                    }
                    else
                        await page.DisplayAlert("Failed", "Notes Failed to be save", "Ok");
                }
            }
        }
        private async void DeleteLyric(object obj)
        {
            var page = App.Current.MainPage;
            var response = await page.DisplayAlert("Warning", "Are you sure you want to Delete this?", "Yes", "No");

            if (response)
            {
                 Lyric.IsDeleted = true;
                var deleted = Lyric.IsDeleted;
                DataAccess.Update(Lyric);

                if (deleted)
                {
                    await page.DisplayAlert("Success", "Lyrics have been successfully Deleted", "Ok");
                }
                else
                {
                    await page.DisplayAlert("Failed", "Lyrics have Failed to be Deleted", "Ok");
                };

            }
            else
            {
                var continueEdit = await page.DisplayAlert("Notice", "Do you want to Continue Editing ", "Yes", "No");
                if (continueEdit != true)
                {
                    await page.Navigation.PopAsync();
                }
            }
        }
        #endregion
    }
}
