using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridgePlanner.Models
{
    public class FridgeItem
    {
        public int FridgeItemId { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public string Unit { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
