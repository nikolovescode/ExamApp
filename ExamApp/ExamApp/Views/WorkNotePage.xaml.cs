using ExamApp.Models;
using ExamApp.Services;
using ExamApp.ViewModels;
using Rg.Plugins.Popup.Services;
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
	public partial class WorkNotePage : ContentPage
    {
        public ObservableCollection<WorkNote> WorkNotes;
        private WorkTask _workTask;    

        public WorkNotePage (WorkTask w)
		{
            WorkNotes = new ObservableCollection<WorkNote>();
            _workTask = w;
            FindWorkNotes();
            InitializeComponent();

        }

        private async void FindWorkNotes()
        {
            ApiServices apiServices = new ApiServices();
            var shifts = await apiServices.FindWorkNoteSubject(Settings.UserName, _workTask.TitleWorkTask);
            foreach (var shift in shifts)
            {
                WorkNotes.Add(shift);
            }

            LvTasks.ItemsSource = WorkNotes;

        }

        private void AddNote_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new AddWorkNote(_workTask));
        }

        private void LvTasks_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}