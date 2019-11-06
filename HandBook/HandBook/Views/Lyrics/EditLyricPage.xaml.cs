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

namespace HandBook
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditLyricPage : ContentPage
	{
		Lyric selectedLyric;
		private LyricsViewModel _context;
		public EditLyricPage ( Lyric selectedLyric)
		{
			InitializeComponent ();
			_context = new LyricsViewModel()
			{
				Lyric = selectedLyric,
			};
			this.selectedLyric = _context.Lyric;
			BindingContext = _context.Lyric;

		}

		public EditLyricPage()
		{
			InitializeComponent();
		}

	async private void BtnDelete_Clicked(object sender, EventArgs e)
		{

			var response = await DisplayAlert("Warning", "Are you sure you want to Delete this?", "Yes", "No");

			if (response)
			{
				var deleted = Crud.DeleteItem(selectedLyric);
				
				if (deleted)
				{
					await DisplayAlert("Success", "Lyrics have been successfully Deleted", "Ok");
				}
				else
				{
					await DisplayAlert("Failed", "Lyrics have Failed to be Deleted", "Ok");
				};
				
			}
			else
			{
				var continueEdit = await DisplayAlert("Notice", "Do you want to Continue Editing ", "Yes", "No");
				if (continueEdit != true)
				{
					await Navigation.PopAsync();
				}
			}
		}

	  
	async private void BtnUpdate_Clicked(object sender, EventArgs e)
	{
		

		if(selectedLyric != null)
		{
			var updated = Crud.Update(selectedLyric);

			if (updated)

			{
				await DisplayAlert("Success", "Lyrics have been successfully Deleted", "Ok");
			}
			else
			{
				await DisplayAlert("Failed", "Lyrics have Failed to be Deleted", "Ok");
			};
				
		}
		else
		{
			var lyric = new Lyric()
			{
				Title = txtTitle.Text,
				Chorus = txtChorus.Text,
				Genre = txtGenre.Text,
				Verse = txtVerse.Text,
				PubDate = DateTime.Today,
			};

			var save = await Crud.SaveAsync(lyric);

			if(save)
			{
			   await DisplayAlert("Success", "Lyrics have been successfully Saved", "Ok");
			}
			else
			{
			   await DisplayAlert("Failed", "Lyrics have Failed to be saved", "Ok");
			};
			
		}
	}
		
	}
}