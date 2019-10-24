using ExamApp.Models;
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
	public partial class WorkshiftDetailPage : ContentPage
	{
        PlannedWorkshift _shift;

		public WorkshiftDetailPage (PlannedWorkshift shift)
		{
            _shift = shift;
			InitializeComponent ();
            lblTimeToWork.Text = "Tid för start "+shift.Hour + ":" + shift.Minute;
            lblLengthOfWorkshift.Text = "Längd på arbetspass "+shift.MinutesToWork.ToString() +" min";

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            lblTitleWorkTask.Text = _shift.TitleWorkTask;

        }

        private async void SetReminder_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ReminderPage(_shift));

        }

        private async void StartShift_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WorkshiftTimerPage(_shift));

        }
    }
}