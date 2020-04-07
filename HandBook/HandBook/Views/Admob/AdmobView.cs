
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HandBook.Views.Admob
{
    public class AdmobView : View
    {
        public static readonly BindableProperty AdUnitIdProperty = BindableProperty.Create(
               nameof(AdUnitId),
               typeof(string),
               typeof(AdmobView),
               string.Empty);

        public string AdUnitId
        {
            get => (string)GetValue(AdUnitIdProperty);
            set => SetValue(AdUnitIdProperty, value);
        }
    }
}
