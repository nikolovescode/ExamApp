using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using ExamApp.Models;
using ExamApp.Views;
using ExamApp.Services;

namespace ExamApp.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<WorkTask> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<WorkTask>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, WorkTask>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as WorkTask;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                ApiServices apiServices = new ApiServices();

                var tasks = await apiServices.FindWorkTasks();

                foreach (var task in tasks)
                {    
                    Items.Add(task);
                }
                
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