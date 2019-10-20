using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandBook.Models;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HandBook
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddTaskPage : ContentPage
	{
		public AddTaskPage ()
		{
			InitializeComponent ();
		}

        async private void BtnSave_Clicked(object sender, EventArgs e)
        {

            ToDoTask tmpTask = new ToDoTask()
            {
                Title = txtTitle.Text,
                Notify = txtNotify.On,
                Start = txtStart.Date,
                StartTime = txtStart.Time,
                End = txtEnd.Date ,
                EndTime = txtEnd.Time,
                Completed = false,
            };

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<ToDoTask>();
                int rows = conn.Insert(tmpTask);

                if (rows > 0)
                {
                   await DisplayAlert("Success", "Task has been saved", "Ok");
                   await Navigation.PopAsync();
                }
                else
                {
                   await DisplayAlert("Failed", "Task have Failed to be saved", "Ok");
                };

            }

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}