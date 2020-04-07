using HandBook.Core.Functions;
using HandBook.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HandBook.ViewModels
{
    public class NoteDetailViewModel :BaseViewModel
    {
        private Note note;
        private List<Note> _notes;
        private CancellationTokenSource canceTokenS;

        #region Properties
        public Note Note
        { get=> note ;
            set
            {
                if (note == value)
                    return;
                else
                {
                    note = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public List<Note> Notes
        {
            get
            {
                return _notes
              .Where(n => n.IsDeleted == false)
              .OrderByDescending(b => b.Id)
              .ToList();
            }
            set 
            {
                if (_notes == value)
                    return;
                else
                { 
                    _notes = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region Commands
        public Command   ReadCommand{ get; set; }
        public Command DeleteCommand { get; set; }
        public Command ShareTextCommand { get; set; }
        public Command SaveToFileCommand { get; set; }
        public Command CopyAllCommand { get; set; }
        public Command EditCommand { get; set; }

        #endregion

        #region Constuctors
        public NoteDetailViewModel(int id)
        {
            FetchList();

            Note = DataAccess.GetNoteById(id);

            ReadCommand = new Command(OnSpeakClick);
            DeleteCommand = new Command(OnDeleteButtonClicked);
            ShareTextCommand = new Command(OnShareButtonClicked);
            SaveToFileCommand = new Command(OnSaveToFileButtonClicked);
            CopyAllCommand = new Command(OnCopyAllButtonClicked);
            EditCommand = new Command(OnEditButtonClicked);

        }
        #endregion

        #region Methods
        private async void OnEditButtonClicked(object obj)
        {
            var app = App.Current.MainPage;
            await app.Navigation.PushAsync(new NewNotesPage(Note.Id));
        }
        private async void OnCopyAllButtonClicked(object obj)
        {
            var clipText = Note.Title + "\n" + Note.Body;
            await Clipboard.SetTextAsync(clipText);
        }
        private void OnSaveToFileButtonClicked(object obj)
        {
            //var name = note.Title + ".txt";
            //var content = $"Title : {note.Title} \n Date : {note.PubDate} \n {note.Body}";
            //string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), name);
            //File.WriteAllText(fileName, content);
        }
        private async void OnShareButtonClicked(object obj)
        {
            var content = $"Title : {note.Title} \n Date : {note.PubDate} \n {note.Body}";
            var shareText = new ShareExt();
            await shareText.ShareText(content);
        }
        public async void OnSpeakClick(object obj)
        {
           await SpeakNow();

        }
        private async void OnDeleteButtonClicked(object obj)
        {
            var app = App.Current.MainPage;
            var response = await app.DisplayAlert("Warning", "Are you sure you want to Delete this note", "Yes", "No");


            if (response)
            {
                Note.IsDeleted = true;
                //var deleted = DataAccess.Delete(note);
                if (Note.IsDeleted)
                {
                    await app.DisplayAlert("Success", "Notes have been successfully Deleted", "Ok");
                }
                else
                {
                    await app.DisplayAlert("Failed", "Notes have Failed to be Deleted", "Ok");
                };
                await app.Navigation.PopAsync();
            }
            else
            {
                var continueEdit = await app.DisplayAlert("Continue ", "Do you want to Continue editing ?", "Yes", "No");
                if (continueEdit == false)
                {
                    await app.Navigation.PopAsync();
                }
            }
        }
        public async Task SpeakNow()
        {
            canceTokenS = new CancellationTokenSource();

            var locales = await TextToSpeech.GetLocalesAsync();
            var locale = locales.Where(b => b.Country == "South Africa").SingleOrDefault();

            var settings = new SpeechOptions()
            {
            
                Locale = locale
            };

            if(Note != null)
            {
                await TextToSpeech.SpeakAsync(Note.Title+ " " + Note.Body, settings,cancelToken: canceTokenS.Token);
            }
        }
        // Cancel speech if a cancellation token exists & hasn't been already requested.
        public void CancelSpeech()
        {
            if (canceTokenS?.IsCancellationRequested ?? true)
                return;

            canceTokenS.Cancel();
        }
        public void FetchList() => Notes = DataAccess.LoadNotes();
        public bool TableExists() => Notes.Count <= 0;

        #endregion
    }
}
