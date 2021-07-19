using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPPAttendance.Views;
using Xamarin.Forms;

namespace PPPAttendance
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void OnService_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Attendance(), animated: true);
        }

        async void OnReports_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Reports(), animated: true);
        }
    }
}