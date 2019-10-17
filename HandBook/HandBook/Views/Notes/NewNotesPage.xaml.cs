﻿using HandBook.Models;
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
	public partial class NewNotesPage : ContentPage
	{
		Note selectedNote;
		public NewNotesPage ()
		{
			InitializeComponent ();

		}

		public NewNotesPage(Note selectedNote)
		{
			InitializeComponent();
			this.selectedNote = selectedNote;
			txtTitle.Text = selectedNote.Title;
			txtNewNote.Text = selectedNote.Body;
		}

		async private void BtnSave_Clicked(object sender, EventArgs e)
		{
			if(selectedNote != null)
			{
				selectedNote.Title = txtTitle.Text;
				selectedNote.Body = txtNewNote.Text;
				using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
				{
					conn.CreateTable<Note>();
					int rows = conn.Update(selectedNote);

					if (rows > 0)
					{
					   await DisplayAlert("Success", "Notes have been successfully updated", "Ok");
					}
					else
					{
					   await DisplayAlert("Failed", "Notes have Failed to be updated", "Ok");
					};

				}
			}
			else
			{
				Note note = new Note()
				{
					Title = txtTitle.Text,
					Body = txtNewNote.Text
				};


				using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
				{
					conn.CreateTable<Note>();
					int rows = conn.Insert(note);

					if (rows > 0)
					{
						await DisplayAlert("Success", "Notes have been successfully Saved", "Ok");
					}
					else
					{
						await DisplayAlert("Failed", "Notes have Failed to be saved", "Ok");
					};

				}
			}

		}

	

		
	}
}