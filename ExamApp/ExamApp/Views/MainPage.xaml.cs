using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExamApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            BarBackgroundColor = Color.FromHex("#c287e8");
            BarTextColor = Color.Black;

        }
    }
}