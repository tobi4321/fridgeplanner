using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridgePlanner.Models
{
    public abstract class Item
    {

        public string Name { get; set; }
        public double Amount { get; set; }
        public string Unit { get; set; }
    }
}
