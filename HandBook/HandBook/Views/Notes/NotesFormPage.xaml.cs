using Amporis.Xamarin.Forms.ColorPicker;
using HandBook.ViewModels.Notes;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HandBook.Views.Notes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotesFormPage : ContentPage
    {
        private NotesFormViewModel viewModel;
        void SetBackground()
        {
            var app = Application.Current as App;
            titleEntry.BackgroundColor = app.EditorBackgroundColor;
            bodyEditor.BackgroundColor = app.EditorBackgroundColor;
        }
        public NotesFormPage()
        {


            InitializeComponent();
            SetBackground();
            viewModel = new NotesFormViewModel();
            BindingContext = viewModel;
        }

        public NotesFormPage(int id)
        {
            

            InitializeComponent();
            SetBackground();
            viewModel = new NotesFormViewModel(id);
            BindingContext = viewModel;
        }


        private async void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
           await (BindingContext as NotesFormViewModel).SetBackgroundColor(rootx);
            var app = Application.Current as App;
            SetBackground();
        }

        private void DeleteItem_Clicked(object sender, EventArgs e)
        {

        }
    }
}