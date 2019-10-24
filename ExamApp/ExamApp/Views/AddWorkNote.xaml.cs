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
	public partial class AddWorkNote
	{
        private WorkTask _workTask;

		public AddWorkNote (WorkTask workTask)
		{
            _workTask = workTask;
			InitializeComponent ();
		}

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            ApiServices apiServices = new ApiServices();

            var workNote = new WorkNote()
            {
                TitleWorkNote = entWorkNote.Text,
                TitleWorkTask = _workTask.TitleWorkTask,
                CalendarUserEmail = Settings.UserName
            };
            bool response = await apiServices.RegisterWorkTask(workNote);
            await PopupNavigation.Instance.PopAsync(true);
        }
    }
}