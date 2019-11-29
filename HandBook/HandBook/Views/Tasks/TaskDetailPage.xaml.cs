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
	public partial class TaskDetailPage : ContentPage
	{
		ToDoTask selectedTask;
		public TaskDetailPage ( ToDoTask task)
		{
			InitializeComponent ();
			 this.selectedTask = task;
			BindingContext = selectedTask ;
		}

		async private void btnDelete_Clicked(object sender, EventArgs e)
		{

			using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
			{
				conn.CreateTable<ToDoTask>();
				int rows = conn.Delete(selectedTask);

				var response = await DisplayAlert("Warning", "Are you Sure?", "Yes", "No");

				if (response)
				{
					if (rows > 0)
					{
						await DisplayAlert("Success", "Task has been updated", "Ok");
						await Navigation.PopAsync();
					}
					else
					{
						await DisplayAlert("Failed", "Task have Failed to be saved", "Ok");
					};
				}
				

			}
		}

		async private void BtnSave_Clicked(object sender, EventArgs e)
		{
			selectedTask.Title = txtTitle.Text;
			selectedTask.Notify = txtNotify.On;
			selectedTask.Start = txtStart.Date;
			selectedTask.End = txtEnd.Date;
			selectedTask.Detail = txtDetail.Text;

			using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
			{
				conn.CreateTable<ToDoTask>();
				int rows = conn.Update(selectedTask);

				if (rows > 0)
				{
					await DisplayAlert("Success", "Task has been updated", "Ok");
					await Navigation.PopAsync();
				}
				else
				{
					await DisplayAlert("Failed", "Task have Failed to be saved", "Ok");
				};

			}
		}
	}
}