using Microsoft.Maui.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarpenTech.Services.SecureServiceStorage
{
    // Implementation of the ISecureStorageService interface using Xamarin.Essentials SecureStorage.
    public class SecureStorageService : ISecureStorageService
    {
        public async Task<dynamic> GetLastUpdateDateTimeOffSet(string date)
        {
            var data = await SecureStorage.Default.GetAsync(date);
            return data;
        }

        public async Task<string> Getsync(string key)
        {
            var data = await SecureStorage.Default.GetAsync(key);
            return data;
        }

        /// <summary>
        /// Retrieves a token securely from the storage based on the specified key.
        /// </summary>
        /// <param name="key">The key associated with the token.</param>
        /// <returns>A Task that resolves to the token value or null if not found.</returns>
        public async Task<string> GetToken(string key)
        {
            // Retrieve the token asynchronously from the secure storage.
            var data = await SecureStorage.Default.GetAsync(key);
            return data;
        }

        public bool RemoveLastUpdateDateTimeOffSet(string key)
        {
            var removeFromStorage =  SecureStorage.Default.Remove(key);
            return removeFromStorage;
        }



        /// <summary>
        /// Removes a token securely from the storage based on the specified key.
        /// </summary>
        /// <param name="key">The key associated with the token to be removed.</param>
        /// <returns>True if the token was successfully removed, false otherwise.</returns>
        public bool RemoveToken(string key)
        {
            // Remove the token from the secure storage and return the result.
            var removeFromStorage = SecureStorage.Default.Remove(key);
            return removeFromStorage;
        }

        public async Task SaveLastUpdateDateTimeOffSet(DateTimeOffset date)
        {
            await SecureStorage.Default.SetAsync("LastUpdateDateTimeOffSet", date.ToString());
        }

        public async Task savesync(string key, string value)
        {
            await SecureStorage.Default.SetAsync(key, value);
        }

        /// <summary>
        /// Saves a token securely in the storage with the specified key.
        /// </summary>
        /// <param name="key">The key under which the token will be stored.</param>
        /// <param name="value">The token value to be stored.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SaveToken(string key, string value)
        {
            // Save the token asynchronously in the secure storage.
            await SecureStorage.Default.SetAsync(key, value);
        }
    }
}
