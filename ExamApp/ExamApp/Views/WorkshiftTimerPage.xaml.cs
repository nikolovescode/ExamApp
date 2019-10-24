using ExamApp.Models;
using ExamApp.Services;
using Plugin.Vibrate;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExamApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WorkshiftTimerPage : ContentPage
	{
        Stopwatch stopwatch;

        PlannedWorkshift _shift;
		public WorkshiftTimerPage (PlannedWorkshift shift)
		{
            _shift = shift;
			InitializeComponent ();
            stopwatch = new Stopwatch();

        }

        protected async  override void OnAppearing()
        {
            base.OnAppearing();
            stopwatch.Start();
            var v = CrossVibrate.Current;
            uint timeToWork = (uint) _shift.MinutesToWork * 60 * 1000;
            await MainProgressBar.ProgressTo(1.0, timeToWork, Easing.Linear);
            stopwatch.Stop();
            stopwatch.Reset();
            DependencyService.Get<IMessage>().LongAlert("Nu har tiden gått ut!");

            v.Vibration(TimeSpan.FromSeconds(1));

        }

        private async void StopTimerButton_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<IMessage>().ShortAlert(stopwatch.Elapsed.ToString());
            await MainProgressBar.ProgressTo(0.0, 0, Easing.Linear);
            long timeWorkedMilliS = stopwatch.ElapsedMilliseconds;
            stopwatch.Stop();
            stopwatch.Reset();
            await PopupNavigation.Instance.PushAsync(new CheckEffectivity(timeWorkedMilliS, _shift));


        }

        protected async override void OnDisappearing()
        {
            base.OnDisappearing();
            await MainProgressBar.ProgressTo(0.0, 0, Easing.Linear);
            stopwatch.Stop();
            stopwatch.Reset();


        }
    }
}