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
	public partial class EditLyricPage : ContentPage
	{
        Lyric selectedLyric;
		public EditLyricPage ( Lyric selectedLyric)
		{
			InitializeComponent ();
            this.selectedLyric = selectedLyric;
            BindingContext = selectedLyric;
		}

        public EditLyricPage()
        {
            InitializeComponent();
        }

    async private void BtnDelete_Clicked(object sender, EventArgs e)
        {
            selectedLyric.Title = txtTitle.Text;
            selectedLyric.Chorus = txtChorus.Text;
            selectedLyric.Verse = txtChorus.Text;
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
                }
            }
            else
            {
                var continueEdit = await DisplayAlert("Notice", "Do you want to Continue Editing ", "Yes", "No");
                if (continueEdit != true)
                {
                    await Navigation.PopAsync();
                }
            }
        }

      
    async private void BtnUpdate_Clicked(object sender, EventArgs e)
    {
        

        if(selectedLyric != null)
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<Lyric>();
                    int rows = conn.Update(selectedLyric);

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
            else
            {
                Lyric lyric = new Lyric()
                {
                    Title = txtTitle.Text,
                    Chorus = txtChorus.Text,
                    Verse = txtVerse.Text,
                };
                    
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<Lyric>();
                    int rows = conn.Insert(lyric);

                    if(rows > 0)
                    {
                       await DisplayAlert("Success", "Lyrics have been successfully Saved", "Ok");
                    }
                    else
                    {
                       await DisplayAlert("Failed", "Lyrics have Failed to be saved", "Ok");
                    };
                }
            }
    }
        
    }
}