using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HandBook.Extensions
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DateCell : ViewCell
	{
        public static readonly BindableProperty LabelProperty = BindableProperty.Create("Label", typeof(string), typeof(DateCell));
        public static readonly BindableProperty DateProperty = BindableProperty.Create("Date", typeof(DateTime), typeof(DateCell));
        public static readonly BindableProperty TimeProperty = BindableProperty.Create("Time", typeof(DateTime), typeof(DateCell));

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public DateTime Date
        {
            get { return (DateTime) GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }

        public DateTime Time
        {
            get { return (DateTime)GetValue(TimeProperty); }
        }

		public DateCell ()
		{
			InitializeComponent ();
            BindingContext = this;
		}
        
	}
}