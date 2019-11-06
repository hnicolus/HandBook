﻿using HandBook.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Data;
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
	public partial class NoteDetailPage : ContentPage
	{

		Note selectedNote;
		private NotesViewModel _context;
		public NoteDetailPage(Note selectedNote)
		{
			InitializeComponent();
			this.selectedNote = selectedNote;
			BindingContext = selectedNote;
			_context = new NotesViewModel()
			{
				Note = selectedNote,
			};
			_context.FetchList();

		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			BindingContext = _context.Notes.SingleOrDefault(b => b.Id == selectedNote.Id);
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
				var deleted = Crud.DeleteItem(selectedNote);
					if (deleted)
					{
						await DisplayAlert("Success", "Notes have been successfully Deleted", "Ok");
					}
					else
					{
						await DisplayAlert("Failed", "Notes have Failed to be Deleted", "Ok");
					};
					await Navigation.PopAsync();
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