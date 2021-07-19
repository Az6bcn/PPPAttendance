using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace PPPAttendance.Views
{
    public partial class Attendance : ContentPage
    {
        public readonly string today;
        public Attendance()
        {
            InitializeComponent();

            today = DateTime.Now.ToString("D");

            BindingContext = this;

            Device.BeginInvokeOnMainThread(() =>
            {
                labelDate.Text = today;
            });
        }
    }
}
