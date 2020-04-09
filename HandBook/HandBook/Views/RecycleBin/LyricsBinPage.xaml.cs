using HandBook.Models;
using HandBook.ViewModels.Recyclebin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HandBook.Views.RecycleBin
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LyricsBinPage : ContentPage
	{
		public LyricsBinPage ()
		{
			InitializeComponent ();
			LoadContent();	
		}

		void LoadContent()=> BindingContext = new LyricsBinViewModel();

		//Restore Tapped item
		private void NotesList_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			(BindingContext as LyricsBinViewModel).ItemTappedCommand
			.Execute(e.Item as Lyric);
			lyricsList.SelectedItem = null;
		}
			
		protected override void OnAppearing()
		{
			LoadContent();
			base.OnAppearing();
		}
	}
}