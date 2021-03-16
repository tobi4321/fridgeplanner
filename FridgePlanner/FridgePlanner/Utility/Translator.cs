using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FridgePlanner.Utility
{
    public static class Translator
    {

        public static string TranslateText(string inputText,string src, string trg)
        {
            using (WebClient client = new WebClient())
            {
                string url = "https://translate.googleapis.com/translate_a/single?client=gtx&sl=" + src + "&tl=" + trg + "&dt=t&q=" + Uri.EscapeDataString(inputText);
                string s = client.DownloadString(url);

                string result = ParserFunction(s);

                return result;
            }
        }

        public static string ParserFunction(string json)
        {
            JArray jsonArray = JArray.Parse(json);
            dynamic test = jsonArray[0][0][0];

            return test;
        }
    }
}
