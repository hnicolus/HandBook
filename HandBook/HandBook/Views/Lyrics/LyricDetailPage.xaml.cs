using HandBook.Models;
using System;
using System.Linq;
using HandBook.Core.Functions;
using HandBook.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HandBook
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LyricDetailPage : ContentPage
    {
        private Lyric _lyric;
        private  LyricsViewModel _context;
        public LyricDetailPage(Lyric selectedLyric)
        {
            InitializeComponent();

            _lyric = selectedLyric;
            _context = new LyricsViewModel()
            {
                Lyric = selectedLyric
            };
           
            BindingContext = _context.Lyric;
        }

        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {

            var response = await DisplayAlert("Warning", "Are you sure you want to Delete this?", "Yes", "No");

            if (response)
            {
                var deleted = DataAccess.Delete(_context.Lyric);
                
                    if (!deleted)
                    {
                        await DisplayAlert("Success", "Lyrics have been successfully Deleted", "Ok");
                    }
                    else
                    {
                        await DisplayAlert("Failed", "Lyrics have Failed to be Deleted", "Ok");
                    };

                    await Navigation.PopAsync();
            }

        }

         private async void BtnEdit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditLyricPage(_context.Lyric));
        }

        protected override void OnAppearing()
        {
       
            _context.FetchList();
            BindingContext = _context.Lyrics.SingleOrDefault( b => b.Id == _lyric.Id);
            base.OnAppearing();

        }

        private void BtnFavourite_Clicked(object sender, EventArgs e)
        {
            if (sender is MenuItem noteItem)
            {
                if (noteItem.CommandParameter is Note note &&
                    (note.IsFavourite ? note.IsFavourite = false : note.IsFavourite = true)) 
                {
                }
            }
        }
        private async void btnCoppyAll_Clicked(object sender, EventArgs e)
        {
            var clipText = _context.Lyric.Title + "\n" +"Genre : " + _context.Lyric.Genre + "\nChorus :\n " +
                _context.Lyric.Chorus + "\n Verses : \n" +_context.Lyric.Verse;
            await Clipboard.SetTextAsync(clipText);
        }
    }
}