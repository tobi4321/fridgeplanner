using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridgePlanner.Utility
{
    public interface IApiCaller
    {
        Task<JObject> GetItem(string routeAttributes);
        Task<JArray> GetList(string routeAttributes);
        Task<JObject> Post(string routeAttributes, object data);
        Task<JObject> Delete(string routeAttributes);
        Task<JObject> Update(string routeAttributes, object data);
    }
}
