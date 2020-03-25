using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.IdentityModel.Tokens;
using System;

namespace Mog.Data
{
    public class ApplicationHelper
    {
        public static CloudBlobContainer ConfigureBlobContainer(string account, string key)
        {
            StorageCredentials storageCredentials = new StorageCredentials(account, key);
            CloudStorageAccount cloudStorageAccount = new CloudStorageAccount(storageCredentials, true);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            var container = cloudBlobClient.GetContainerReference("images");
            return container;
        }

        public static bool VerifyToken(SecurityToken token, string issuer)
        {
            var status = 0;
            status = (token.ValidTo > DateTime.Now) ? 1 : 0;
            status = (token.Issuer == issuer) ? 1 : 0;
            return (status == 1) ? true : false;
        }

        public static bool CheckForToken(string token)
        {
            return (token.Length > 0) ? true : false;
        }
    }
}