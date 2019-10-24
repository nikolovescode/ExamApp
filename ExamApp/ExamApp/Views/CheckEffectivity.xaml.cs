using ExamApp.Models;
using ExamApp.Services;
using Rg.Plugins.Popup.Services;
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
	public partial class CheckEffectivity 
	{
        private long _timeWorkedMilliS;
        private PlannedWorkshift _shift;

        public CheckEffectivity (long timeWorkedMilliS, PlannedWorkshift shift)
		{
            _timeWorkedMilliS = timeWorkedMilliS;
            _shift = shift;
			InitializeComponent ();
		}

        private async void YesButton_Clicked(object sender, EventArgs e)
        {
            ApiServices apiServices = new ApiServices();

            var workshift = new Workshift()
            {
                IdWorkTask = _shift.IdWorkTask,
                TitleWorkTask = _shift.TitleWorkTask,
                PlannedWorkshiftId = _shift.Id,
                CalendarUserEmail = Settings.UserName,
                WasEffective = true,
                MinutesWorking = (int) (_timeWorkedMilliS/1000)/60
            };
            bool response = await apiServices.RegisterWorkshift(workshift);
            await PopupNavigation.Instance.PopAsync(true);
        }

        private async void NoButton_Clicked(object sender, EventArgs e)
        {
            ApiServices apiServices = new ApiServices();

            var workshift = new Workshift()
            {
                IdWorkTask = _shift.IdWorkTask,
                TitleWorkTask = _shift.TitleWorkTask,
                PlannedWorkshiftId = _shift.Id,
                CalendarUserEmail = Settings.UserName,
                WasEffective = false,
                MinutesWorking = (int)(_timeWorkedMilliS / 1000) / 60
            };
            bool response = await apiServices.RegisterWorkshift(workshift);
            await PopupNavigation.Instance.PopAsync(true);
        }
    }
}