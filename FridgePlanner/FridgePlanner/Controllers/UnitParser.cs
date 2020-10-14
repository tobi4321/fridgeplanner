using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridgePlanner.Controllers
{
    public static class UnitParser
    {
        public static double KgToG(double kg)
        {
            double g = kg * 1000;
            return g;
        }
        public static double GToKg(double g)
        {
            double kg = g / 1000;
            return kg;
        }
        public static double LToMl(double l)
        {
            double ml = l * 1000;
            return ml;
        }
        public static double MlToL(double ml)
        {
            double l = ml / 1000;
            return l;
        }

    }
}
