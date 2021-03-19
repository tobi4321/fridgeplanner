using FridgePlanner.Models;
using FridgePlanner.Models.NutritionModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace FridgePlanner.Utility
{
    public class NutritionApiHandler
    {
        public NutritionApiHandler()
        {
        }


        public NutritionAPIResponse sendRequest([FromServices]IConfiguration config,NutritionRequest requestData)
        {

            NutritionConfig conf = config.GetSection("NutritionConfig").Get<NutritionConfig>();

            var url = "https://api.edamam.com/api/nutrition-details?app_id=" + conf.ApiId + "&app_key=" + conf.ApiKey;

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(createJsonData(requestData));
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    NutritionAPIResponse response = Newtonsoft.Json.JsonConvert.DeserializeObject<NutritionAPIResponse>(result);
                    return response;
                }
            }
            else
            {
                // should be error model
                return null;
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
