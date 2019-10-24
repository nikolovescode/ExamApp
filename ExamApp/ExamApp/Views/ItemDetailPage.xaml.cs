using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ExamApp.Models;
using ExamApp.ViewModels;
using ExamApp.Services;
using System.Threading.Tasks;
using System.Numerics;

namespace ExamApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;
        bool _compareToMe;
        private int _avg;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {

            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
            _compareToMe = false;
            CalculateAvg(_compareToMe);
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new WorkTask
            {
                TitleWorkTask = "WorkTask 1",
                Description = "This is an item description."
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            PrintInterestGroup(_compareToMe);
        }

        private async void CalculateAvg(bool _compareToMe)
        {
            int totalInstances = 0;
            int totalWorkedTime = 0;

           ApiServices apiServices = new ApiServices();

            if (_compareToMe == false)
            {
                var shiftsSubject = await apiServices.FindWorkshiftsSubject(viewModel.Item.TitleWorkTask);
                if (shiftsSubject.Count > 0)
                {
                    foreach (var shift in shiftsSubject)
                    {
                        totalInstances = totalInstances + 1;

                        totalWorkedTime = totalWorkedTime + shift.MinutesWorking;
                    }
                    int averageWorking = totalWorkedTime / totalInstances;
                    _avg = averageWorking;
                    lblAverage.Text = _avg.ToString() + " min";
                    TestTime(averageWorking);
                }
                else
                {
                    lblAverage.Text = "0 min";
                }
            }
            else
            {
                var shiftsSubject = await apiServices.FindWorkshiftsSubjectOfUser(Settings.UserName, viewModel.Item.TitleWorkTask);
                if (shiftsSubject.Count > 0)
                {
                    foreach (var shift in shiftsSubject)
                    {
                        totalInstances = totalInstances + 1;

                        totalWorkedTime = totalWorkedTime + shift.MinutesWorking;
                    }
                    int averageWorking = totalWorkedTime / totalInstances;
                    _avg = averageWorking;
                    lblAverage.Text = _avg.ToString() + " min";
                    TestTime(averageWorking);
                }
                else
                {
                    lblAverage.Text = "0 min";
                }
            }
        }

        private async void TestTime(float averageWorking)
        {
            ApiServices apiServices = new ApiServices();
            int xTotalCases = 0;
            int xEffCases = 0;
            int xIneffCases = 0;
            double effLikelyhood = 0;
            double ineffLikelyhood = 0;
            int totalEffectiveInstances = 0;
            int totalIneffectiveInstances = 0;
            double xEffProbability = 0;
            double xEffProbabilityMultEffLikelyhood = 0;
            double xIneffProbability = 0;
            double xIneffProbabilityMultIneffLikelyhood = 0;
            double possibilityOfX = 0;

            var shiftsSubject = await apiServices.FindWorkshiftsSubjectOfUser(Settings.UserName, viewModel.Item.TitleWorkTask);
            if (shiftsSubject.Count > 0)
            {
                foreach (var shift in shiftsSubject)
                {
                    if (shift.WasEffective == true)
                    {
                        totalEffectiveInstances = totalEffectiveInstances + 1;
                        if (shift.MinutesWorking > averageWorking)
                        {
                            xEffCases = xEffCases + 1;

                        }

                    }
                    else
                    {
                        totalIneffectiveInstances = totalIneffectiveInstances + 1;
                        if (shift.MinutesWorking > averageWorking)
                        {
                            xIneffCases = xIneffCases + 1;

                        }
                    }
                    if (shift.MinutesWorking > averageWorking)
                    {
                        xTotalCases = xTotalCases + 1;
                    }

                }


                    //Steg 1: Kolla sannolikhet för effektivt/ineffektivt av alla fall
                    effLikelyhood = (double) totalEffectiveInstances / shiftsSubject.Count;
                ineffLikelyhood = (double) totalIneffectiveInstances / shiftsSubject.Count;

                //Steg 2: Sannolikheten att ett pass upplevs som effektivt/ineffektivt när användaren arbetar efter x
             
                    xEffProbability = (double) xEffCases/totalEffectiveInstances;
                xEffProbabilityMultEffLikelyhood = (double) xEffProbability * effLikelyhood;
                xIneffProbability = (double)xIneffCases / totalIneffectiveInstances;
                xIneffProbabilityMultIneffLikelyhood = (double)xIneffProbability * ineffLikelyhood;
                //Steg 3: För att normalisera divideras båda sidor av beviset med sannolikheten för särdraget som vi är intresserade av, P(x)
                possibilityOfX = (double) xTotalCases / shiftsSubject.Count;
                double effResult = (double) xEffProbabilityMultEffLikelyhood/ possibilityOfX;
                double ineffResult = (double) xIneffProbabilityMultIneffLikelyhood / possibilityOfX;

                if (effResult > ineffResult)
                {
                  lblRecommendation.Text = "Tips: Arbeta längre än " + _avg + " min";

                 }

            }


        }

        private float LikelyhoodCheck(int numberOfTimes, int totalNumberOfEvents)
        {
            return numberOfTimes / totalNumberOfEvents;
        }

        private void PrintInterestGroup(bool _compareToMe)
        {   
            if (_compareToMe == false)
            {
                lblInterest.Text="Genomsnitt för alla användare";
            }
            else
            {
                lblInterest.Text = "Ditt genomsnitt";
            }
        }
        private void PrintAvg(float avg)
        {

                lblAverage.Text = avg.ToString() + " min";
      
        }

        private void PrintRec(float avg, bool _moreThanAvg)
        {
            if (_moreThanAvg == true) { 
            lblRecommendation.Text= "Tips: Arbeta längre än "+avg.ToString()+ " min";
            }
        }
        private async void ViewNotes_Clicked(object sender, EventArgs e)
        {
            WorkTask w = viewModel.Item;

            await Navigation.PushAsync(new WorkNotePage(w));
        }

        private async void AddToCalendar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage(viewModel.Item)));

        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            _compareToMe = e.Value;

            PrintInterestGroup(_compareToMe);

            CalculateAvg(_compareToMe);

        }
    }
}