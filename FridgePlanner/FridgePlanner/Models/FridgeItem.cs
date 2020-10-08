using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridgePlanner.Models
{
    public class FridgeItem : Item
    {
        public int Id { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
