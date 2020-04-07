
using HandBook.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HandBook
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoteDetailPage : ContentPage
    {
        int _id;
        private  NoteDetailViewModel _context;
        public NoteDetailPage(int id)
        {
            _id = id;
            InitializeComponent();
            Refresh(id);
        }

        private void Refresh(int id)
        {
            _context = new NoteDetailViewModel(id);
            BindingContext = _context;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Refresh(_id);

        }

        protected override bool OnBackButtonPressed()
        {
            _context.CancelSpeech();
            return base.OnBackButtonPressed();
        }
    }
}