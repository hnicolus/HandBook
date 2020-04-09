using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HandBook.Core.Functions;
using HandBook.Models;
using Xamarin.Forms;

namespace HandBook.ViewModels
{
    public class LyricsViewModel :BaseViewModel
    {
        #region Fields
        Page page = Application.Current.MainPage;
        private List<Lyric> lyrics;
        private bool tableExists;
        #endregion

        public List<Lyric> Lyrics
        {
            get=> lyrics;
            set 
            {
                if (lyrics == value)
                    return;
                lyrics = value;
                NotifyPropertyChanged("Lyrics");
            }
        }
        public bool TableExist
        {
            get => tableExists;
            set
            {
                if (tableExists == value)
                    return;
                tableExists = value;
                NotifyPropertyChanged("TableExist");
            }
        }

        #region Command
        public Command DeleteItemCommand { get; set; }
        public Command ItemTappedCommand { get; set; }
        public ICommand AddLyricCommand { get; set; }
        #endregion

        #region Constructors
        public LyricsViewModel()
        {
            page = Application.Current.MainPage as Page;
            AddLyricCommand = new Command(AddNewLyrics);
            ItemTappedCommand = new Command(async it => await ItemTapped(it as Lyric));
          
            DeleteItemCommand = new Command( async item => await OnDeleteItemButtonClicked(item as Lyric));
            FetchList();
            TableExists();

        }

        public async Task OnDeleteItemButtonClicked(Lyric lyric)
        {
            var response = await page.DisplayAlert("Warning", "Are you sure you want to Delete this?", "Yes", "No");

            if (response)
            {
                var delete = Delete(lyric);

                if (delete)
                {
                    await page.DisplayAlert("Success", "Lyrics have been successfully Deleted", "Ok");
                }
                else
                {
                    await page.DisplayAlert("Failed", "Lyrics have Failed to be Deleted", "Ok");
                }
                FetchList();
            }
        }

        private async void AddNewLyrics(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new EditLyricPage());
        }

        private async Task ItemTapped(Lyric TappedLyric)
        {
            //Do Something
            await Application.Current.MainPage.Navigation.PushAsync(new LyricDetailPage(TappedLyric));
        }
        private void FetchList()
        {
            lyrics = DataAccess.LoadLyrics().OrderBy(b => b.Id)
                .Where(b => b.IsDeleted == false).ToList()
            ;
        }

        private void TableExists()
        {
            TableExist = (Lyrics.Count <= 0);
        }

        private bool Delete(Lyric item)
        {
            item.IsDeleted = true;
            var isDelete = DataAccess.Update(item);
            return isDelete;
        }
        #endregion




    }
}
