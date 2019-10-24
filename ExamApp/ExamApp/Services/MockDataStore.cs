using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamApp.Models;

namespace ExamApp.Services
{
    public class MockDataStore : IDataStore<WorkTask>
    {
        List<WorkTask> items;

        public MockDataStore()
        {
            items = new List<WorkTask>();
            var mockItems = new List<WorkTask>
            {
                new WorkTask { Id = Guid.NewGuid().ToString(), TitleWorkTask = "First item", Description="This is an item description." },
                new WorkTask { Id = Guid.NewGuid().ToString(), TitleWorkTask = "Second item", Description="This is an item description." },
                new WorkTask { Id = Guid.NewGuid().ToString(), TitleWorkTask = "Third item", Description="This is an item description." },
                new WorkTask { Id = Guid.NewGuid().ToString(), TitleWorkTask = "Fourth item", Description="This is an item description." },
                new WorkTask { Id = Guid.NewGuid().ToString(), TitleWorkTask = "Fifth item", Description="This is an item description." },
                new WorkTask { Id = Guid.NewGuid().ToString(), TitleWorkTask = "Sixth item", Description="This is an item description." },
            };

            foreach (var item in mockItems)
            {
                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(WorkTask item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(WorkTask item)
        {
            var oldItem = items.Where((WorkTask arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((WorkTask arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<WorkTask> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<WorkTask>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}