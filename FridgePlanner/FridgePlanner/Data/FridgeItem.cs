using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FridgePlanner.Data
{
    public class FridgeItem : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public double Amount { get; set; }
        public string Unit { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ExpiryDate { get; set; }
    }
}
