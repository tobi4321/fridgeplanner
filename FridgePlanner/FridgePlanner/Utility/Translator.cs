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
        //Wir umgehen die Benutzung einer teuren Übersetzungs Api durch Verwendung des Google Translators wie er auch über den Browser verfügbar wäre

        public static string TranslateText(string inputText,string sourceLanguage, string targetLanguage)
        {
            //create WebClient to donwload the requested result 
            using (WebClient client = new WebClient())
            {
                //Form the request string url like Googletranslator requires and concat the inputtext as Uri to it
                string url = "https://translate.googleapis.com/translate_a/single?client=gtx&sl=" + sourceLanguage + "&tl=" + targetLanguage + "&dt=t&q=" + Uri.EscapeDataString(inputText);
                //send request to url 
                string s = client.DownloadString(url);

                string result = ExtractTranslatorResult(s);

                return result;
            }
        }

        public static string ExtractTranslatorResult(string json)
        {
            //The Result String is a threedimensional JArray
            JArray jsonArray = JArray.Parse(json);
            //The translated text is at position [0][0][0]
            dynamic test = jsonArray[0][0][0];

            return test;
        }
    }
}
