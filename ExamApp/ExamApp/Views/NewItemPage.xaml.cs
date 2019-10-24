using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ExamApp.Models;
using ExamApp.ViewModels;
using ExamApp.Services;

namespace ExamApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        public WorkTask Item { get; set; }

        private DateTime _chosenDate;
        WorkTask _item;
        bool _isToggled;
        int _hourToWork;
        int _minToWork;

        public NewItemPage(WorkTask item)
        {
            InitializeComponent();

            Item = new WorkTask
            {
                TitleWorkTask = "WorkTask name",
                Description = "This is an item description."
            };
            BindingContext = this;
            this._item = item;
            lblWorkshift.Text = _item.TitleWorkTask;
            _isToggled = false;
            _hourToWork = 0;
            _minToWork = 0;
            PresentTime();

        }
        private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            _chosenDate = e.NewDate;
            DisplayAlert(e.NewDate.ToString(), "Valt värde", "OK");

        }
        async void Save_Clicked(object sender, EventArgs e)
        {
            ApiServices apiServices = new ApiServices();

            var plannedWorkshift = new PlannedWorkshift()
            {
                IdWorkTask = Convert.ToInt32(_item.Id),
                TitleWorkTask = _item.TitleWorkTask,
                CalendarUserEmail = Settings.UserName,
                MinutesToWork = Convert.ToInt32(entLengthOfWork.Text),
                Minute = _minToWork,
                Hour = _hourToWork,
                Day = _chosenDate.Day,
                Month = _chosenDate.Month,
                Year = _chosenDate.Year,
                Priority = _isToggled,
                Done = false
            };
            bool response = await apiServices.RegisterPlannedWorkshift(plannedWorkshift);
            await Navigation.PopModalAsync();
        }


        private void ViewTasksButton_Clicked(object sender, EventArgs e)
        {

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            PrintPriority(_isToggled);
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            _isToggled = e.Value;

            PrintPriority(_isToggled);

        }

        private void PrintPriority(bool p)
        {
            if (p == false)
            {
                lblPriority.Text = "Ej prioriterat";
            }
            else
            {
                lblPriority.Text = "Prioriterat";
            }
        }

        private void HourSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {           
            double theF = HourSlider.Value;
            _hourToWork = Convert.ToInt32(theF);
            PresentTime();
        }

        private void PresentTime()
        {
            HourLabel.Text = "Timslag för start: "+_hourToWork.ToString()+":"+ _minToWork.ToString();
        }

        private void MinuteSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            double theF = MinuteSlider.Value;
            _minToWork = Convert.ToInt32(theF);
            PresentTime();
        }
    }
}                                                                            