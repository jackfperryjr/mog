using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Mog.Api.Core.Abstractions;
using Mog.Api.Core.Models;

namespace Mog.Api.Core.Extensions
{
    public class ApplicationExtensions
    {
        public static CloudBlobContainer ConfigureBlobContainer(string account, string key)
        {
            // Configures container based on credentials passed in.
            var storageCredentials = new StorageCredentials(account, key);
            var cloudStorageAccount = new CloudStorageAccount(storageCredentials, true);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            var container = cloudBlobClient.GetContainerReference("images");
            return container;
        }

        public static async Task<T> Get<T>(string user)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync("https://chocobo.moogleapi.com/v1/manage/get/" + user);
                result.EnsureSuccessStatusCode();
                string resultString = await result.Content.ReadAsStringAsync();
                T resultContent = JsonConvert.DeserializeObject<T>(resultString);
                return resultContent;
            }
        }
    }
}
