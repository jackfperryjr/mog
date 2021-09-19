using System.Threading.Tasks;
using System.Net.Http;
using Azure.Storage;
using Azure.Storage.Blobs;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;

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

        public static string ModifyOriginForSearch(string origin)
        {
            switch (origin) 
            {
                case "1":
                case "01":
                case "i":
                case "I":
                case "one":
                case "final fantasy i":
                case "final fantasy 1":
                case "final fantasy one":
                    origin = "Final Fantasy I";
                    break;
                case "2":
                case "02":
                case "two":
                case "ii":
                case "II":
                case "final fantasy ii":
                case "final fantasy 2":
                case "final fantasy two":
                    origin = "Final Fantasy II";
                    break;
                case "3":
                case "03":
                case "three":
                case "iii":
                case "III":
                case "final fantasy iii":
                case "final fantasy 3":
                case "final fantasy three":
                    origin = "Final Fantasy III";
                    break;
                case "4":
                case "04":
                case "four":
                case "iv":
                case "IV":
                case "final fantasy iv":
                case "final fantasy 4":
                case "final fantasy four":
                    origin = "Final Fantasy IV";
                    break;
                case "5":
                case "05":
                case "five":
                case "v":
                case "V":
                case "final fantasy v":
                case "final fantasy 5":
                case "final fantasy five":
                    origin = "Final Fantasy V";
                    break;
                case "6":
                case "06":
                case "six":
                case "vi":
                case "VI":
                case "final fantasy vi":
                case "final fantasy 6":
                case "final fantasy six":
                    origin = "Final Fantasy VI";
                    break;
                case "7":
                case "07":
                case "seven":
                case "vii":
                case "VII":
                case "final fantasy vii":
                case "final fantasy 7":
                case "final fantasy seven":
                    origin = "Final Fantasy VII";
                    break;
                case "8":
                case "08":
                case "eight":
                case "viii":
                case "VIII":
                case "final fantasy viii":
                case "final fantasy 8":
                case "final fantasy eight":
                    origin = "Final Fantasy VIII";
                    break;
                case "9":
                case "09":
                case "nine":
                case "ix":
                case "IX":
                case "final fantasy ix":
                case "final fantasy 9":
                case "final fantasy nine":
                    origin = "Final Fantasy IX";
                    break;
                case "10":
                case "ten":
                case "x":
                case "X":
                case "final fantasy x":
                case "final fantasy 10":
                case "final fantasy ten":
                    origin = "Final Fantasy X";
                    break;
                case "10-2":
                case "10 2":
                case "ten-2":
                case "ten 2":
                case "x-2":
                case "x2":
                case "X-2":
                case "X2":
                case "final fantasy x2":
                case "final fantasy 10-2":
                case "final fantasy ten 2":
                    origin = "Final Fantasy X-2";
                    break;
                case "12":
                case "twelve":
                case "xii":
                case "XII":
                case "final fantasy xii":
                case "final fantasy 12":
                case "final fantasy twelve":
                    origin = "Final Fantasy XII";
                    break;
                case "13":
                case "thirteen":
                case "xiii":
                case "XIII":
                case "final fantasy xiii":
                case "final fantasy 13":
                case "final fantasy thirteen":
                    origin = "Final Fantasy XIII";
                    break;
                case "13-2":
                case "13 2":
                case "thirteen-2":
                case "thirteen 2":
                case "xiii-2":
                case "xiii2":
                case "XIII-2":
                case "XIII2":
                case "final fantasy xiii2":
                case "final fantasy 13-2":
                case "final fantasy thirteen 2":
                    origin = "Final Fantasy X-2";
                    break;
                case "14":
                case "fourteen":
                case "xiv":
                case "XIV":
                case "final fantasy xiv":
                case "final fantasy 14":
                case "final fantasy fourteen":
                    origin = "Final Fantasy XIV";
                    break;
            }
            return origin;
        }
    }
}
