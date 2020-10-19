using FridgePlanner.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace FridgePlanner.Controllers
{
    public class NutritionApiHandler
    {
        private readonly IOptions<NutritionConfig> _serviceSettings;

        public NutritionApiHandler(IOptions<NutritionConfig> serviceSettings)
        {
            _serviceSettings = serviceSettings;
        }

        public NutritionApiHandler()
        {
        }

        public void sendRequest(NutritionRequest requestData)
        {
            var url = "https://api.edamam.com/api/nutrition-details?app_id=" + "2164c490" + "&app_key=" + "5ab8c54eae13f177841c04de882fb217";

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(createJsonData(requestData));
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                Console.WriteLine(result);
            }
        }

        public string createJsonData(NutritionRequest data)
        {
            string json = "{";

            // set title
            json = json + '"' + "title" + '"' + ':' + '"' + data.title +'"' + ',';

            // set ingredients
            json = json + '"' + "ingr" + '"' + ':' + '[';
            for (int i = 0; i < data.ingr.Count; i ++)
            {
                if (i < data.ingr.Count - 1)
                {
                    json =  json + '"' + data.ingr.ElementAt(i)+ '"' + ',';
                }
                else
                {
                    json =  json + '"' + data.ingr.ElementAt(i) + '"' ;
                }
            }

            // close ingr and json
            json = json + ']' + '}';

            return json;
        }


    }
}
