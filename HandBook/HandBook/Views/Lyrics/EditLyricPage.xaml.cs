using HandBook.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandBook.Core.Functions;
using HandBook.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HandBook.ViewModels.Lyrics;

namespace HandBook
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditLyricPage : ContentPage
	{
		private LyricsFormViewModel _context;
		public EditLyricPage ( Lyric selectedLyric)
		{
			InitializeComponent ();
			_context = new LyricsFormViewModel(selectedLyric.Id,new PageService());
			BindingContext = _context;
		}

		public EditLyricPage()
		{
			InitializeComponent();
			_context = new LyricsFormViewModel(new PageService());
			BindingContext = _context;
		}

	 
		
	}
}