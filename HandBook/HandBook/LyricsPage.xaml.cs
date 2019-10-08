using HandBook.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HandBook
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LyricsPage : ContentPage
    {
        public LyricsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
           base.OnAppearing();

           using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {

                conn.CreateTable<Lyric>();
                var list = conn.Table<Lyric>().ToList<Lyric>();
                refresh();
              
           };
        }

        private void refresh()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Lyric>();
                var notes = conn.Table<Lyric>().ToList();
                if (notes.Count <= 0)
                {
                    btnTopAdd.IsVisible = true;
                }
                else
                {
                    btnTopAdd.IsVisible = false;
                }
                LyricsList.ItemsSource = notes;

            }
        }
        async private void BtnDelete_Clicked(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;

            var lyric = menuItem.CommandParameter as Lyric;
            var response = await DisplayAlert("Warning", "Are you sure you want to Delete this?", "Yes", "No");

            if (response)
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<Lyric>();
                    int rows = conn.Delete(lyric);

                    if (rows > 0)
                    {
                       await DisplayAlert("Success", "Lyrics have been successfully Deleted", "Ok");
                        
                    }
                    else
                    {
                       await DisplayAlert("Failed", "Lyrics have Failed to be Deleted", "Ok");
                    };
                }
            }
            refresh();

        }

        async private void BtnAddLyric_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditLyricPage());
        }

       async  private void LyricsList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (LyricsList.SelectedItem is Lyric selectedLyric)
            {
                await Navigation.PushAsync(new LyricDetailPage(selectedLyric));
            }
        }

        private void LyricsList_Refreshing(object sender, EventArgs e)
        {
            refresh();
            LyricsList.EndRefresh();
        }
    }
}

