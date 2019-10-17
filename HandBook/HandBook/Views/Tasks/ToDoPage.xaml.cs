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
	public partial class ToDoPage : ContentPage
	{
		public ToDoPage ()
		{
			InitializeComponent ();


		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
			{
				refreshItems();
			}
		}



		private void TaskList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var selectedTask = taskList.SelectedItem as ToDoTask;

			if (selectedTask != null)
			{
				Navigation.PushAsync(new TaskDetailPage(selectedTask));
			};
		}


		private void refreshItems()
		{
			using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
			{
				conn.CreateTable<ToDoTask>();
				var task = conn.Table<ToDoTask>().ToList();
				taskList.ItemsSource = task;
			}
			taskList.EndRefresh();
		}

		private void TaskList_Refreshing(object sender, EventArgs e)
		{
			refreshItems();


		}

		async private void BtnDelete_Clicked(object sender, EventArgs e)
		{
			var menuItem = sender as MenuItem;

			var task = menuItem.CommandParameter as ToDoTask;
			using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
			{
				conn.CreateTable<ToDoTask>();
				int rows = conn.Delete(task);
				var response = await DisplayAlert("Warning", "Are you sure you to do Delete Task ?", "Yes", "No");
				if (response)
				{
					if (rows > 0)
					{
						await DisplayAlert("Success", "Task has been successfully Deleted", "Ok");
					}
					else
					{
						await DisplayAlert("Failed", "Task Failed to be Deleted", "Ok");
					};
				}
				refreshItems();

			}
		}
		async private void BtnCompleted_Clicked(object sender, EventArgs e)
		{
			var menuItem = sender as MenuItem;
			var task = menuItem.CommandParameter as ToDoTask;
			task.Completed = true;

			using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
			{
				conn.CreateTable<ToDoTask>();
				int rows = conn.Update(task);
			  
					if (rows > 0)
					{
					   await DisplayAlert("Completed", "Task has been successfully Completed","ok");
					
				  
					}
					else
					{
						await DisplayAlert("Failed", "Task Failed to be Deleted", "Ok");
					};

			}
			refreshItems();

		}

		async private void BtnNewTask_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new AddTaskPage());
		}
	}
}