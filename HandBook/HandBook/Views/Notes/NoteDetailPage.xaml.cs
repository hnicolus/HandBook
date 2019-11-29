using HandBook.Models;
using SQLite;
using System;
using System.IO;
using Xamarin.Forms.Internals;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandBook.Core.Functions;
using HandBook.ViewModels;
using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;

namespace HandBook
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoteDetailPage : ContentPage
    {
        private Note _selectedNote;
        private  NotesViewModel _context;
        public NoteDetailPage(Note selectedNote)
        {
            InitializeComponent();
            _selectedNote = selectedNote;
            BindingContext = selectedNote;
            _context = new NotesViewModel()
            {
                Note = selectedNote,
            };
            Refresh();
        }

        private void Refresh()
        {

            _context.FetchList();
        }
        protected override void OnAppearing()
        {
            Refresh();
            BindingContext = _context.Notes.SingleOrDefault(b => b.Id == _selectedNote.Id);
            base.OnAppearing();

        }

        private async void BtnEdit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewNotesPage(_selectedNote));
        }

         private async void BtnDelete_Clicked(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Warning", "Are you sure you want to Delete this note", "Yes", "No");


            if (response)
            {
                var deleted = DataAccess.Delete(_selectedNote);
                if (deleted)
                {
                    await DisplayAlert("Success", "Notes have been successfully Deleted", "Ok");
                }
                else
                {
                    await DisplayAlert("Failed", "Notes have Failed to be Deleted", "Ok");
                };
                await Navigation.PopAsync();
            }
            else
            {
                var continueEdit = await DisplayAlert("Continue ", "Do you want to Continue editing ?", "Yes", "No");
                if (continueEdit == false)
                {
                    await Navigation.PopAsync();
                }
            }
        }

        private async void btnCopyAll_Clicked(object sender, EventArgs e)
        {
            var clipText = _context.Note.Title + "\n" + _context.Note.Body;
           await Clipboard.SetTextAsync(clipText);
        }

        private void btnSaveToTxt_Clicked(object sender, EventArgs e)
        {
            var name = _selectedNote.Title + ".txt";
            var content = $"Title : {_selectedNote.Title} \n Date : {_selectedNote.PubDate} \n {_selectedNote.Body}";
            string fileName = Path.Combine(@"storage/emulated/0/Documents/", name);
            File.WriteAllText(fileName, content);
        }
    }
}