using System;

using ExamApp.Models;

namespace ExamApp.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public WorkTask Item { get; set; }
        public ItemDetailViewModel(WorkTask item = null)
        {
            Title = item?.TitleWorkTask;
            Item = item;
        }
    }
}
