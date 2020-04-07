using HandBook.Models;
using HandBook.ViewModels;
using System;
using HandBook.Core.Functions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HandBook.ViewModels.Notes;

namespace HandBook
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewNotesPage : ContentPage
    {
        private NotesFormViewModel viewModel;

        public NewNotesPage()
        {
            InitializeComponent();
            viewModel = new NotesFormViewModel();
            BindingContext = viewModel;
        }

        public NewNotesPage(int id)
        {
            InitializeComponent();
            viewModel = new NotesFormViewModel(id);
            BindingContext = viewModel;
        }

    }
}