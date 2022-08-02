using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Helpers
{
    public static class APIRequester
    {
        public static async Task<object> GetRequest(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                using (var Response = await client.GetAsync(url))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return Response.Content.ReadAsStringAsync().Result;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public static async Task<object> PostRequest(string url,object data)
        {
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                using (var Response = await client.PostAsync(url, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return Response.Content.ReadAsStringAsync().Result;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
    }
}
