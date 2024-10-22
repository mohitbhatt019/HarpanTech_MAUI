using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarpenTech.Services.SecureServiceStorage
{
    // Interface defining methods for securely storing and retrieving tokens.
    public interface ISecureStorageService
    {
        /// <summary>
        /// Saves a token securely in the storage with the specified key.
        /// </summary>
        /// <param name="key">The key under which the token will be stored.</param>
        /// <param name="value">The token value to be stored.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        Task SaveToken(string key, string value);

        /// <summary>
        /// Retrieves a token securely from the storage based on the specified key.
        /// </summary>
        /// <param name="key">The key associated with the token.</param>
        /// <returns>A Task that resolves to the token value or null if not found.</returns>
        Task<string> GetToken(string key);

        /// <summary>
        /// Removes a token securely from the storage based on the specified key.
        /// </summary>
        /// <param name="key">The key associated with the token to be removed.</param>
        /// <returns>True if the token was successfully removed, false otherwise.</returns>
        bool RemoveToken(string key);

        Task SaveLastUpdateDateTimeOffSet(DateTimeOffset date);
        Task<dynamic> GetLastUpdateDateTimeOffSet(string date);
        bool RemoveLastUpdateDateTimeOffSet(string key);

        Task savesync(string key, string value);
        Task<string> Getsync(string key);


    }
}
