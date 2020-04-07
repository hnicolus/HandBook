using HandBook.Models;
using SQLite;
using Syncfusion.SfSchedule.XForms;
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

			}
		}

		//private void refreshItems()
		//{
		//	using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
		//	{
		//		conn.CreateTable<ToDoTask>();
		//		var task = conn.Table<ToDoTask>().ToList();
		//		//taskList.ItemsSource = task;
		//	}
		//	//taskList.EndRefresh();
		//}

		//private void TaskList_Refreshing(object sender, EventArgs e)
		//{
		//	refreshItems();


		//}
		async private void BtnNewTask_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new AddTaskPage());
		}


		//Change Calender View
		private void calenderViewOption_SelectedIndexChanged(object sender, EventArgs e)
		{
			schedule.ScheduleView  = (ScheduleView) calenderViewOption.SelectedIndex;

		}
	}
}