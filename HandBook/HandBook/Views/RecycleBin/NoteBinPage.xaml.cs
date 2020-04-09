using HandBook.Models;
using HandBook.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandBook.Core.Functions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HandBook.ViewModels.Recyclebin;

namespace HandBook.Views.RecycleBin
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NoteBinPage : ContentPage
	{
		private RecyclebinViewModel ctx;
		public NoteBinPage()
		{

			InitializeComponent();

			Refresh();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			Refresh();
		}

		private void Refresh()
		{
			ctx = new RecyclebinViewModel();
			BindingContext = ctx;
		}

		private  void NotesList_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			var note = e.Item as Note;	
			(BindingContext as RecyclebinViewModel).ItemTappedCommand.Execute(note);
			NotesList.SelectedItem = null;

		}

	}
}