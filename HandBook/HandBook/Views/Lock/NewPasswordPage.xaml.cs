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
	public partial class NewPasswordPage : ContentPage
	{
		public NewPasswordPage ()
		{
			InitializeComponent ();
		}

		async private void Button_Clicked(object sender, EventArgs e)
		{
			var prop = Application.Current as App;
			if(password1.Text == password2.Text)
			{
				prop.Password = password1.Text;
				prop.NickName = txtNickName.Text;
				prop.FavouriteColor = txtColor.Text;
				await Navigation.PopAsync();
			}
			else
			{
				await DisplayAlert("Error", "Passwords Do not match please Try again", "Ok");
			}
		}
	}
}