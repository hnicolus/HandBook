using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace HandBook.Views.EditorSettings
{
    /// <summary>
    /// Page to show the setting.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditorSettingsPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditorSettingsPage" /> class.
        /// </summary>
        public EditorSettingsPage()
        {
            InitializeComponent();
        }
    }
}