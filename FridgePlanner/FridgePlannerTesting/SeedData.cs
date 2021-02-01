using FridgePlanner.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FridgePlannerTesting
{
    public static class SeedData
    {
        public static void PopulateTestData(DataBaseContext dbContext)
        {
            dbContext.FridgeItems.Add(new FridgeItem() { Id = 1,Name = "Tomaten", Amount = 1.5, Unit = "Kg"});
            dbContext.FridgeItems.Add(new FridgeItem() { Id = 2, Name = "Bier", Amount = 2, Unit = "Kisten" });
            dbContext.SaveChanges();
        }
    }
}
