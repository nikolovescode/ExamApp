using ExamApp.Models;
using ExamApp.Services;
using Plugin.Vibrate;
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
	public partial class ReminderPage : ContentPage
	{
        PlannedWorkshift _shift;
		public ReminderPage (PlannedWorkshift shift)
		{
            _shift = shift;
			InitializeComponent ();
		}

        protected async  override void OnAppearing()
        {
            base.OnAppearing();
            DateTime endTime = new DateTime(_shift.Year, _shift.Month, _shift.Day, _shift.Hour, _shift.Minute, 0);
            DateTime startTime = DateTime.Now;
            TimeSpan span = endTime.Subtract(startTime);            

            var v = CrossVibrate.Current;

            await MainProgressBar.ProgressTo(1.0, (uint) span.Minutes*60*1000, Easing.Linear);
            DependencyService.Get<IMessage>().LongAlert("Nu börjar arbetspasset!");
            if (_shift.Priority == true)
            {
                DependencyService.Get<IMessage>().LongAlert("Passet är prioriterat");
            }

            v.Vibration(TimeSpan.FromSeconds(1));
        }
    }
}