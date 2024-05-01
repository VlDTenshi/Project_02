using MonkeyCache.FileStore;
using Newtonsoft.Json;
using Project_02.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Project_02.Services
{
    public class InternetMedicineService
    {
        static string BaseUrl = "My URL";

        static HttpClient client;

        static InternetMedicineService()
        {
            try
            {
                client = new HttpClient
                {
                    BaseAddress = new Uri(BaseUrl)
                };
            }
            catch
            {

            }
        }

        public static Task<IEnumerable<Medicine>> GetMedicine() =>
            GetAsync<IEnumerable<Medicine>>("api/Medicine", "getmedicine");

        static async Task<T> GetAsync<T>(string url, string key, int mins = 1, bool forceRefresh = false)
        {
            var json = string.Empty;

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                json = Barrel.Current.Get<string>(key);
            else if (!forceRefresh && !Barrel.Current.IsExpired(key))
                json = Barrel.Current.Get<string>(key);

            try
            {
                if (string.IsNullOrWhiteSpace(json))
                {
                    json = await client.GetStringAsync(url);

                    Barrel.Current.Add(key, json, TimeSpan.FromMinutes(mins));
                }
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get information from server {ex}");
                throw ex;
            }
        }

        static Random random = new Random();
       

        public static async Task AddMedicine(string name, string type)
        {
            var image = "";
            var medicine = new Medicine
            {
                Name = name,
                Type = type,
                Image = image,
                Id = random.Next(0, 10000)
            };

            var json = JsonConvert.SerializeObject(medicine);
            var content =
                new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/Medicine", content);

            if (!response.IsSuccessStatusCode)
            {

            }
        }

        public static async Task RemoveMedicine(int id)
        {
            var response = await client.DeleteAsync($"api/Medicine/{id}");
            if (!response.IsSuccessStatusCode)
            {

            }
        }
    }
}
