using ExamApp.Models;
using ExamApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExamApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WorkshiftPage : ContentPage
	{
        public ObservableCollection<PlannedWorkshift> PlannedWorkshifts;
        DateTime _chosenDate;

        public WorkshiftPage (DateTime chosenDate)
		{
            PlannedWorkshifts = new ObservableCollection<PlannedWorkshift>();
            _chosenDate = chosenDate;
            FindPlannedWorkshifts();

            InitializeComponent();
		}

        private async  void FindPlannedWorkshifts()
        {

            ApiServices apiServices = new ApiServices();
            var shifts = await apiServices.FindDatesPlannedWorkshifts(Settings.UserName, false, _chosenDate);
            foreach (var shift in shifts)
            {
                PlannedWorkshifts.Add(shift);
            }


            LvTasks.ItemsSource = PlannedWorkshifts;


        }

        private async void LvTasks_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as PlannedWorkshift;
            if (item == null)
                return;

            await Navigation.PushAsync(new WorkshiftDetailPage(item));

            // Manually deselect item.
            LvTasks.SelectedItem = null;
        }
    }
}