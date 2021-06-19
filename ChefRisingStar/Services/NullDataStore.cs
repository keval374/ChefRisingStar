using ChefRisingStar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChefRisingStar.Services
{
    public class NullDataStore : IDataStore<bool, byte>
    {   
        public async Task<bool> AddItemAsync(byte item)
        {
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(byte item)
        {   
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(byte id)
        {
            return await Task.FromResult(true);
        }

        public async Task<bool> GetItemAsync(byte id)
        {
            return await Task.FromResult(true);
        }
       
        public Task<bool> AddItemAsync(bool item)
        {
            return Task.FromResult(true);
        }

        public Task<bool> UpdateItemAsync(bool item)
        {
            return Task.FromResult(true);
        }

        Task<IEnumerable<bool>> IDataStore<bool, byte>.GetItemsAsync(bool forceRefresh)
        {
            throw new NotImplementedException();
        }
    }
}