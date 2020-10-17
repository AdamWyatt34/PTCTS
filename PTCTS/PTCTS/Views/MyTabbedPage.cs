using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PTCTS.Views
{
    public class MyTabbedPage : TabbedPage
    {
        public static readonly BindableProperty IsHiddenProperty = BindableProperty.Create("IsHidden", typeof(bool), typeof(MyTabbedPage), false);

        public bool IsHidden
        {
            set { SetValue(IsHiddenProperty, value); }
            get { return (bool)GetValue(IsHiddenProperty); }
        }
    }
}
