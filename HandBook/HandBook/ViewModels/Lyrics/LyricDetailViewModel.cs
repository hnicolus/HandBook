using HandBook.Core.Functions;
using HandBook.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HandBook.ViewModels.Lyrics
{
    public class LyricDetailViewModel :BaseViewModel
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
        public Command BackButtonCommand { get; set; }
        public Command SpeakCommand { get; set; }
        public Command EditCommand { get; set; }
        public Command DeleteCommand { get; set; }
        public Command ShareCommand { get; set; }
        public Command SaveToFileCommand { get; set; }
        #endregion

        #region Constructors
        public LyricDetailViewModel(int id)
        {
            Lyric = DataAccess.GetLyricById(id);
            InstantiateCommands();
        }
        #endregion

        #region Methods

        void InstantiateCommands()
        {
            BackButtonCommand = new Command(OnBackButtonClicked);
            SpeakCommand = new Command(ReadText);
            EditCommand = new Command(OpenEditPage);
            DeleteCommand = new Command(DeleteLyric);
            ShareCommand = new Command(ShareText);
            SaveToFileCommand = new Command(SaveToFile);
        }

        private async void OnBackButtonClicked(object obj)
        {
          await App.Current.MainPage.Navigation.PopAsync();
        }

        private void SaveToFile(object obj)
        {
            var name = Lyric.Title + ".txt";
            var content = $"Title : {Lyric.Title} \n Date : {Lyric.PubDate} \n{Lyric.Chorus}\n {Lyric.Verse}";
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),"HandBookLyrics", name);
            File.WriteAllText(fileName, content);
        }

        private async void ShareText(object obj)
        {
            var content = $"Title : {Lyric.Title} \n Date : {Lyric.PubDate} \n " +
                          $"Chorus :\n{Lyric.Chorus} \n" +
                          $"Verse \n:{Lyric.Verse}";
            var shareText = new ShareExt();
            await shareText.ShareText(content);
        }

        private async void DeleteLyric(object obj)
        {
            var page = App.Current.MainPage;
            var response = await page.DisplayAlert("Warning", "Are you sure you want to Delete this?", "Yes", "No");

            if (response)
            {
                var deleted = DataAccess.Delete(Lyric);

                if (!deleted)
                {
                    await page.DisplayAlert("Success", "Lyrics have been successfully Deleted", "Ok");
                }
                else
                {
                    await page.DisplayAlert("Failed", "Lyrics have Failed to be Deleted", "Ok");
                };

                await page.Navigation.PopAsync();
            }
        }

        private async void OpenEditPage(object obj)
        { 
            await App.Current.MainPage.Navigation.PushAsync(new EditLyricPage(Lyric));
        }

        private async void ReadText(object obj)
        {
            var canceTokenS = new CancellationTokenSource();

            var locales = await TextToSpeech.GetLocalesAsync();
            var locale = locales.Where(b => b.Country == "South Africa").SingleOrDefault();

            var settings = new SpeechOptions()
            {

                Locale = locale
            };

            if (Lyric != null)
            {
                await TextToSpeech.SpeakAsync($"{Lyric.Title}.{Lyric.Chorus}. {Lyric.Verse}", settings, cancelToken: canceTokenS.Token);
            }
        }
        #endregion
    }
}
