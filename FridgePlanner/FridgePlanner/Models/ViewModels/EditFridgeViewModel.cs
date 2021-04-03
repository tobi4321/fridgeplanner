using FridgePlanner.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridgePlanner.Models.ViewModels
{
    public class EditFridgeViewModel
    {
        public FridgeItem Item { get; set; }
        public List<string> Units { get; set; }
    }
}
