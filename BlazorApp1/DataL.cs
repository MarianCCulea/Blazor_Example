using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using BlazorApp1;


public interface IDataL
{
    Task<Root> MovieByName(string name);
}

namespace BlazorApp1
{
    public class DataL : IDataL
    {
        public static DataL instance = null; 
        private static readonly object padlock = new object();
        HttpClient client = new HttpClient();
        public DataL()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

        }

        public static DataL Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new DataL();
                    }
                    return instance;
                }
            }
        }


        public async Task<Root> MovieByName(string name)
        {
            var uri = "https://cloud-computing-sep6.ew.r.appspot.com/search/movie/page/1/name/"+ name;
            var streamTask = await client.GetStringAsync(uri);

            Root recordDetails = JsonSerializer.Deserialize(streamTask.ToString(), typeof(Root)) as Root;
            Root userCopy = JsonSerializer.Deserialize<Root>(streamTask);

            Console.WriteLine(recordDetails.page);
            foreach (var item in recordDetails.results)
            {
                Console.WriteLine(item.title);
            }
            Console.WriteLine("ASDASDASDAS");
            Console.WriteLine(userCopy.page);
            Console.WriteLine(userCopy.page);

            return userCopy;
        }
    }
}
