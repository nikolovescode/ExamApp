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
	public partial class PausePage : ContentPage
	{
		public PausePage ()
		{
			InitializeComponent ();
        }

        private async void StartTimerButton_Clicked(object sender, EventArgs e)
        {
            ApiServices apiServices = new ApiServices();

            var list = await apiServices.FindSettingsPauseEmail(Settings.UserName);
            var v = CrossVibrate.Current;

            await MainProgressBar.ProgressTo(1.0, (uint)list.First().MinPauseBeforeActivity*60*1000, Easing.Linear);
            DependencyService.Get<IMessage>().LongAlert("Slut på pausen!");

            v.Vibration(TimeSpan.FromSeconds(1));

            }

        protected async override void OnDisappearing()
        {
            base.OnDisappearing();
            await MainProgressBar.ProgressTo(0, 0, Easing.Linear);

        }

    }
}