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
            Refresh();
        }

        protected override void OnAppearing()
        {
            Refresh();
            base.OnAppearing();
        
        }

        private void Refresh()
        {
            _context = new LyricsViewModel();
            BindingContext = _context;
        }
    private async void BtnDelete_Clicked(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;

            var lyric = menuItem.CommandParameter as Lyric;
           await (BindingContext as LyricsViewModel).OnDeleteItemButtonClicked(lyric);
            Refresh();

        }

        private async void BtnAddLyric_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditLyricPage());
        }

        void LyricsList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            LyricsList.SelectedItem = null;
        }

        private void LyricsList_Refreshing(object sender, EventArgs e)
        {
            Refresh();
            LyricsList.EndRefresh();
        }

        private void LyricsList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedLyric = e.Item as Lyric;
            (BindingContext as LyricsViewModel).ItemTappedCommand.Execute(selectedLyric);
            LyricsList.SelectedItem = null;
        }
    }
}

