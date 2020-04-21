using System.Threading.Tasks;
using Xamarin.Forms;

namespace HandBook.ViewModels
{
   public class PageService : IPageService
    {
        private Page page = Application.Current.MainPage;
        public async Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
        {
            return await page.DisplayAlert(title, message,accept, cancel);
        }

        public async Task DisplayAlert(string title, string message, string cancel)
        {
           await page.DisplayAlert(title, message, cancel);
        }
        public async Task<Page> PopAsync()
        {
          return   await page.Navigation.PopAsync();
        }

        public async Task PushAsync(Page page)
        {
            await page.Navigation.PushAsync(page);
        }
    }
}
