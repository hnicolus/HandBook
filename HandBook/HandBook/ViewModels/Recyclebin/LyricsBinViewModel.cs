using HandBook.Core.Functions;
using HandBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HandBook.ViewModels.Recyclebin
{
    public class LyricsBinViewModel: BaseViewModel
    {
        #region Fields
        private List<Lyric> lyrics;
        #endregion

        #region Properties
        public List<Lyric> Lyrics
        {
            get => lyrics;
            set
            {
                if (lyrics == value)
                    return;
                lyrics = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Commands
        public Command ItemTappedCommand { get; set; }
        public Command DeleteAllCommand { get; set; }
        public Command BackButtonCommand { get; set; }
        public Command DeleteCommand { get; set; }
        #endregion

        #region Constructors
        public LyricsBinViewModel()
        {
            ItemTappedCommand = new Command(async n => await OnItemTapped(n as Lyric));
            DeleteCommand = new Command(OnDeleteButtonClicked);
            DeleteAllCommand = new Command(OnDeleteAllButtonClicked);
            BackButtonCommand = new Command(OnBackButtonClicked);
            LoadLyrics();
        }
        #endregion

        #region Methods
        private async void OnBackButtonClicked(object obj)
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

        private async void OnDeleteAllButtonClicked(object obj)
        {
            var page = App.Current.MainPage;
            var response = await page.DisplayAlert("Warning", "Clearing the Lyrics Recycle Bin will" +
                                                    " delete all the items " +
                                                    ",Are you sure you want to " +
                                                    "permanently delete all the recycled " +
                                                    "notes?", "Yes", "Cancel");
            if (response)
            {
                foreach (Lyric n in Lyrics)
                {
                    DataAccess.Delete(n);
                }
            }
            LoadLyrics();
        }

        private async Task OnItemTapped(Lyric note)
        {
            var page = App.Current.MainPage;
            var response = await page.DisplayAlert("Alert", "Do you want to Restore Selected File ?", "Yes", "No");
            if (!response) return;
            note.IsDeleted = false;
            DataAccess.Update(note);
            LoadLyrics();
        }


        public void LoadLyrics()
        {
            Lyrics = DataAccess.LoadLyrics().Where(b => b.IsDeleted == true).ToList();
        }

        async void OnDeleteButtonClicked(object obj)
        {
            var app = App.Current.MainPage;
            var sd = obj as Post;
            if (obj is MenuItem menuItem)
            {
                var lyric = menuItem.CommandParameter as Lyric;
                var response = await app.DisplayAlert("Warning", "Are you sure you want to delete this Lyrics ?", "Yes", "No");
                if (response)
                {
                    if (lyric != null)
                    {
   
                        var delete = DataAccess.Delete(lyric);

                        if (delete)
                        {
                            await app.DisplayAlert("Success", "Lyrics have been successfully Deleted", "Ok");
                        }
                        else
                        {
                            await app.DisplayAlert("Failed", "Lyrics have Failed to be Deleted", "Ok");
                        }
                    }

                }
            }

            LoadLyrics();
        }
        #endregion
    }
}
