﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HandBook
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : TabbedPage
	{
		public HomePage ()
		{
			InitializeComponent ();
		}

		async private void BtnAdd_Clicked(object sender, EventArgs e)
		{
		   await Navigation.PushAsync(new NewNotesPage());
		}

		async private void BtnAddTask_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new AddTaskPage());
		}

		async private void BtnAbout_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new Views.About.AboutPage());
		}

		private void BtnShare_Clicked(object sender, EventArgs e)
		{

		}

		async private void BtnSettings_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new Views.Settings.SettingsPage());
		}
	}
}