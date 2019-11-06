using HandBook.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandBook.Core.Functions;
using HandBook.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HandBook
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LyricDetailPage : ContentPage
    {
      
        LyricsViewModel _context;
        public LyricDetailPage(Lyric selectedLyric)
        {
            InitializeComponent();

            _context = new LyricsViewModel()
            {
                Lyric = selectedLyric
            };
           
            BindingContext = _context.Lyric;
        }



        async private void BtnDelete_Clicked(object sender, EventArgs e)
        {

            var response = await DisplayAlert("Warning", "Are you sure you want to Delete this?", "Yes", "No");

            if (response)
            {
                var deleted = Crud.DeleteItem(_context.Lyric);
                
                    if (deleted)
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

        async private void BtnEdit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditLyricPage(_context.Lyric));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _context.FetchList();
        var item =  _context.Lyrics.SingleOrDefault(b => b.Id == _context.Lyric.Id);
        _context.Lyric = item;

        }

        private void BtnFavourite_Clicked(object sender, EventArgs e)
        {
            var noteItem = sender as MenuItem;
            var note = noteItem.CommandParameter as Note;
            if (note.IsFavourite ? note.IsFavourite == false : note.IsFavourite == true) ;
        }
    }
}