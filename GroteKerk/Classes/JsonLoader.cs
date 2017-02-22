using System;
using System.Json;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace GroteKerk.Classes
{
    class JsonLoader
    {
        public async Task<JsonValue> FetchJson(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";

            using (WebResponse response = await request.GetResponseAsync())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    JsonValue json = await Task.Run(() => JsonObject.Load(stream));
                    return json;
                }
            }
        }
    }
}