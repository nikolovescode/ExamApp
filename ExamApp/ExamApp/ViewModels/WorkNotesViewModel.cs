using ExamApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
//Denna klass används för testning

namespace ExamApp.ViewModels
{
    public class WorkNotesViewModel : BaseViewModel
    {
        public ObservableCollection<WorkNote> WorkNotes { get; set; }
        public Command LoadItemsCommand { get; set; }

        public WorkNotesViewModel()
        {
            Title = "Anteckningar";
            WorkNotes = new ObservableCollection<WorkNote>();
           
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                WorkNotes.Clear();
                var items = await DataStore.GetItemsAsync(true);
                WorkNote iOne = new WorkNote();
                iOne.Id = 1;
                iOne.TitleWorkNote = "Lägg i tvättkorg";
                iOne.TitleWorkTask = "Hushållssysslor";
                iOne.CalendarUserEmail = "nikolaj@email.com";
                WorkNote iTwo = new WorkNote();
                iTwo.Id = 2;
                iTwo.TitleWorkNote = "Sätt på tvättmaskin";
                iTwo.TitleWorkTask = "Hushållssysslor";
                iTwo.CalendarUserEmail = "nikolaj@email.com";
                WorkNotes.Add(iTwo);
                WorkNote iThree = new WorkNote();
                iThree.Id = 3;
                iThree.TitleWorkNote = "Torka tvätt";
                iThree.TitleWorkTask = "Hushållssysslor";
                iThree.CalendarUserEmail = "nikolaj@email.com"; WorkNotes.Add(iThree);
                WorkNotes.Add(iThree);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
