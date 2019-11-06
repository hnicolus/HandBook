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
    public partial class LyricsPage : ContentPage
    {
        private LyricsViewModel _context;
        public LyricsPage()
        {
            InitializeComponent();
            _context =new LyricsViewModel();
        }

        protected override void OnAppearing()
        {
           base.OnAppearing();
           _context.FetchList();

        }

        private void refresh()
        {
            _context.FetchList();
            LyricsList.ItemsSource = _context.Lyrics.OrderBy(b => b.Id);
            btnTopAdd.IsVisible = _context.TableExists();
        }
        async private void BtnDelete_Clicked(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;

            var lyric = menuItem.CommandParameter as Lyric;
            var response = await DisplayAlert("Warning", "Are you sure you want to Delete this?", "Yes", "No");

            if (response)
            {
                var delete = Crud.DeleteItem(lyric);

                if (delete)
                {
                   await DisplayAlert("Success", "Lyrics have been successfully Deleted", "Ok");
                    
                }
                else
                {
                   await DisplayAlert("Failed", "Lyrics have Failed to be Deleted", "Ok");
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

