﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridgePlanner.Data
{
    public class ShoppingItem : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public double Amount { get; set; }
        public string Unit { get; set; }
    }
}
