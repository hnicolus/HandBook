using HandBook.Models;
using System;
using System.Linq;
using HandBook.Core.Functions;
using HandBook.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HandBook
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LyricsPage : ContentPage
    {
        
        private  LyricsViewModel _context;
        public LyricsPage()
        {
            InitializeComponent(); 
            _context =new LyricsViewModel();
            BindingContext = _context;
            Refresh();
        }

        protected override void OnAppearing()
        {
            Refresh();
            base.OnAppearing();
        
        }

        private void Refresh()
        {
            
            BindingContext = null;
            _context.FetchList();
            _context.TableExists();
            BindingContext = _context;

        }
    private async void BtnDelete_Clicked(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;

            var lyric = menuItem.CommandParameter as Lyric;
            var response = await DisplayAlert("Warning", "Are you sure you want to Delete this?", "Yes", "No");

            if (response)
            {
                var delete = _context.Delete(lyric);

                if (!delete)
                {
                   await DisplayAlert("Success", "Lyrics have been successfully Deleted", "Ok");
                }
                else
                {
                    await DisplayAlert("Failed", "Lyrics have Failed to be Deleted", "Ok");
                }
            }
            Refresh();

        }

        private async void BtnAddLyric_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditLyricPage());
        }

       private async void LyricsList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            if (LyricsList.SelectedItem is Lyric selectedLyric)
            {
                await Navigation.PushAsync(new LyricDetailPage(selectedLyric));
                LyricsList.SelectedItem = null;
            }
        }

        private void LyricsList_Refreshing(object sender, EventArgs e)
        {
            Refresh();
            LyricsList.EndRefresh();
        }
    }
}

