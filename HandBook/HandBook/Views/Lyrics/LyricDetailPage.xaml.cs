using HandBook.Models;
using System;
using System.Linq;
using HandBook.Core.Functions;
using HandBook.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HandBook.ViewModels.Lyrics;

namespace HandBook
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LyricDetailPage : ContentPage
    {
        private int id;
        private LyricDetailViewModel _context;
        public LyricDetailPage(Lyric selectedLyric)
        {
            InitializeComponent();

            id = selectedLyric.Id;
            LoadContent();

        }

        void LoadContent()
        {
            _context = new LyricDetailViewModel(id);
            BindingContext = _context;
        }
        protected override void OnAppearing()
        {
            LoadContent();
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