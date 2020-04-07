using HandBook.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HandBook.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermsPage : ContentPage
    {
        private TermsAndConditionsViewModel context;
        public TermsPage()
        {
            InitializeComponent();
            context = new TermsAndConditionsViewModel();
            BindingContext = context;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            AppInfoList.SelectedItem = null;

        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}