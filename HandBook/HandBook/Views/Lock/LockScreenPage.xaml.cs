using HandBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HandBook.Views.Lock
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LockScreenPage : ContentPage
	{
		private Lyric selectedLyrics;
		private Note selectedNote;
		public LockScreenPage ()
		{
			InitializeComponent ();
		}

		public LockScreenPage( Note note)
		{
			BindingContext = note;
			InitializeComponent();
			selectedNote = note;
		}

		public LockScreenPage(Lyric lyric)
		{
			InitializeComponent();
			BindingContext = lyric;
			selectedLyrics = lyric;

		}

		async private void BtnView_Clicked(object sender, EventArgs e)
		{
			if(selectedLyrics != null)
			{

				if (selectedLyrics.Islocked)
				{
					await Navigation.PushAsync(new LyricDetailPage(selectedLyrics));
				}
				else
				{
					await DisplayAlert("Error", "Password does not Match", "ok");
				}
			}
			else if(selectedNote != null)
			{

			}
		}
	}

}