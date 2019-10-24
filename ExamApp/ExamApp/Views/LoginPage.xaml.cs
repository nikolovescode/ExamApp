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
	public partial class LoginPage : ContentPage
	{
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void BtnLogin_OnClicked(object sender, EventArgs e)
        {
            ApiServices apiServices = new ApiServices();
            bool response = await apiServices.LoginUser(EntEmail.Text, EntPassword.Text);
            if (!response)
            {
                await DisplayAlert("Varning", "Något gick fel...", "OK");
            }
            else
            {
                Navigation.InsertPageBefore(new MainPage(), this);
                await Navigation.PopAsync();
            }
        }

        private void TapSignUp_OnTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignUpPage());
        }

        private void TapSignUp_Tapped(object sender, EventArgs e)
        {

        }
    }
}
