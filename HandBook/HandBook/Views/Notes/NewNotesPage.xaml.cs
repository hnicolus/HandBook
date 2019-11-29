using HandBook.Models;
using HandBook.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandBook.Core.Functions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HandBook
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewNotesPage : ContentPage
	{
		readonly Note _selectedNote;
		public NewNotesPage ()
		{
			InitializeComponent ();
		}

		public NewNotesPage(Note selectedNote)
		{
			InitializeComponent();

			_selectedNote = selectedNote;
			txtTitle.Text = selectedNote.Title;
			txtNewNote.Text = selectedNote.Body;

		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
		

		}

		async private void BtnSave_Clicked(object sender, EventArgs e)
		{
			if(_selectedNote != null)
			{
				_selectedNote.Title = txtTitle.Text;
				_selectedNote.Body = txtNewNote.Text;

			   var isUpdated =  DataAccess.Update(_selectedNote);

				if (isUpdated)
				{
					await DisplayAlert("Success", "Notes have been successfully Updated", "Ok");
				}else
					await DisplayAlert("Failed", "Notes Failed to be Updated", "Ok");
			}
			else
			{
				Note note = new Note()
				{
					Title = txtTitle.Text,
					Body = txtNewNote.Text,
					PubDate = DateTime.Today,
					IsDeleted = false,
				};

				var saved = DataAccess.Save(note);

				if (saved)
				{
					await DisplayAlert("Success", "Notes has been saved", "Ok");
				}
				else
				{

					await DisplayAlert("Failed", "Notes have Failed to be saved", "Ok");
				}
			}

		}
	}
}