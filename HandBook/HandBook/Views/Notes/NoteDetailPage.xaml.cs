using HandBook.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HandBook
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NoteDetailPage : ContentPage
	{

		Note selectedNote;
		public NoteDetailPage(Note selectedNote)
		{
			InitializeComponent();
			this.selectedNote = selectedNote;
			BindingContext = selectedNote;

		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			BindingContext = selectedNote;
		}

		async private void BtnEdit_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new NewNotesPage(selectedNote));
		}

		async private void BtnDelete_Clicked(object sender, EventArgs e)
		{
			var response = await DisplayAlert("Warning", "Are you sure you want to Delete this note", "Yes", "No");


			if (response)
			{
				using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
				{
					conn.CreateTable<Note>();
					int rows = conn.Delete(selectedNote);

					if (rows > 0)
					{
						await DisplayAlert("Success", "Notes have been successfully Deleted", "Ok");
					}
					else
					{
						await DisplayAlert("Failed", "Notes have Failed to be Deleted", "Ok");
					};
					await Navigation.PopAsync();
				}
			}
			else
			{
				var continueEdit = await DisplayAlert("Continue ", "Do you want to Continue editing ?", "Yes", "No");
				if (continueEdit == false)
				{
					await Navigation.PopAsync();
				}
			}
		}
	}
}