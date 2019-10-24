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
    public partial class LyricDetailPage : ContentPage
    {
        Lyric selectedLyric;
        public LyricDetailPage(Lyric selectedLyric)
        {
            InitializeComponent();

            this.selectedLyric = selectedLyric;
            BindingContext = selectedLyric;
        }



        async private void BtnDelete_Clicked(object sender, EventArgs e)
        {

            var response = await DisplayAlert("Warning", "Are you sure you want to Delete this?", "Yes", "No");

            if (response)
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<Lyric>();
                    int rows = conn.Delete(selectedLyric);

                    if (rows > 0)
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



        }

        async private void BtnEdit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditLyricPage(selectedLyric));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

        }

        private void BtnFavourite_Clicked(object sender, EventArgs e)
        {
            var noteItem = sender as MenuItem;
            var note = noteItem.CommandParameter as Note;
            if (note.IsFavourite == true ? note.IsFavourite == false : note.IsFavourite == true);
        }
    }
}