using HandBook.Core.Functions;
using HandBook.Views.EditorSettings;
using HandBook.Views.RecycleBin;
using HandBook.Views.Settings;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace HandBook.ViewModels.Home
{
 
    /// <summary>
    /// Viewmodel of settings page
    /// </summary>
    [Preserve(AllMembers = true)]
    public class SettingsViewModel
    {
        private Page page = Application.Current.MainPage;
        public SettingsViewModel()
        {
            this.EditorSettingsCommand = new Command(this.EditorSettingsTapped);
            this.RecycleBinCommand = new Command(this.RecycleBinTapped);
            this.ShareAppCommand = new Command(this.ShareAppTapped);
            this.GetMoreAppsCommand = new Command(this.GetMoreAppsTapped);
            this.AboutUsCommand = new Command(this.AboutUsTapped);
            this.TermsCommand = new Command(this.TermsAndConditionsTapped);
            this.PolicyCommand = new Command(this.PrivacyPolicyTapped);
        }


        public Command EditorSettingsCommand { get; set; }
        public Command RecycleBinCommand { get; set; }
        public Command ShareAppCommand { get; set; }
        public Command GetMoreAppsCommand { get; set; }
        public Command AboutUsCommand { get; set; }
        public Command TermsCommand { get; set; }
        public Command PolicyCommand { get; set; }

 

        private async void EditorSettingsTapped(object obj)
        {
            await App.Current.MainPage.Navigation.PushAsync(new EditorSettingsPage());
        }
        private async void RecycleBinTapped(object obj)
        {
            await App.Current.MainPage.Navigation.PushAsync(new RecycleHomePage());
        }
        private async void ShareAppTapped(object obj)
        {
            var shareApp = new ShareExt();
            await shareApp.ShareAppAsync();
        }
        private async void TermsAndConditionsTapped(object obj)
        {
            await App.Current.MainPage.Navigation.PushAsync(new TermsPage());
        }

        private async void AboutUsTapped(object obj)
        {
            await App.Current.MainPage.Navigation.PushAsync(new AboutPage());
        }

        private async void GetMoreAppsTapped(object obj)
        {
            var getMoreApps = new ShareExt();
            await getMoreApps.GetMoreAppsAsync();
        }
        private void PrivacyPolicyTapped(object obj)
        {
        }
    }
}
