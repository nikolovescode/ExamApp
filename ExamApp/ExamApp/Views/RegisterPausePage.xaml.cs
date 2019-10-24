using ExamApp.Models;
using ExamApp.Services;
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
	public partial class RegisterPausePage : ContentPage
	{
        string _email;
		public RegisterPausePage (string email)
		{
            _email = email;
			InitializeComponent ();
		}
        private async void BtnSignUp_OnClicked(object sender, EventArgs e)
        {
            ApiServices apiServices = new ApiServices();

            var pause = new SettingsPause()
            {
                MinPauseBeforeActivity = Convert.ToInt32(EntMin.Text),
                CalendarUserEmail = _email
            };
            bool response = await apiServices.RegisterSettingsPause(pause);
            if (response == true) { 
            await DisplayAlert("Hej", "Tid för vila registrerat!", "Ok");
            await Navigation.PopToRootAsync();
            }
            else
            {
            await DisplayAlert("Oops", "Något gick fel!", "Ok");
                await Navigation.PopToRootAsync();
            }
        }
        }
}