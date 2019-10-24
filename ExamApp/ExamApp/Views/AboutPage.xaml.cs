using System;
using Microcharts;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExamApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        private DateTime _chosenDate;

        public AboutPage()
        {
            _chosenDate = DateTime.Today;
            InitializeComponent();
        }

        private async void ViewWorkshiftButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WorkshiftPage(_chosenDate));

        }

        private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            _chosenDate = e.NewDate;
            DisplayAlert(e.NewDate.ToString(), "Selected Value", "OK");

        }
    }
    }
