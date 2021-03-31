using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridgePlanner.Utility
{
    public class ControllerApiHandler
    {
        private readonly ApiCaller _api;
        public ControllerApiHandler(ApiCaller api)
        {
            _api = api;
        }


    }
}
