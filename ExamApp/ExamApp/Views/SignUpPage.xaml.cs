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
    public partial class SignUpPage : ContentPage
    {
        
            public SignUpPage()
            {
                InitializeComponent();
            }

            private async void BtnSignUp_OnClicked(object sender, EventArgs e)
            {
                ApiServices apiServices = new ApiServices();
                bool response = await apiServices.RegisterUser(EntEmail.Text, EntPassword.Text, EntConfirmPassword.Text);
                if (!response)
                {
                    await DisplayAlert("Oj!", "Något gick fel...", "Stäng");
                }
                else
                {
                    await DisplayAlert("Hej", "Ditt konto har registrerats", "Ok");
                    Navigation.InsertPageBefore(new RegisterPausePage(EntEmail.Text), this);
                    await Navigation.PopAsync();
                }
            }

        }
    }
