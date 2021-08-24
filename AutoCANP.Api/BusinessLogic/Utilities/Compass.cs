using AutoCANP.Api.BusinessLogic.Utilities.IntervalTree;
using System.Collections.Generic;

namespace AutoCANP.Api.BusinessLogic.Utilities
{
    public static class Compass
    {
        public static string DegreesToCompass(double deg)
        {
            var val = (int)System.Math.Floor(deg);
            string[] arr = { "N", "NNE", "NE", "ENE", "E", "ESE", "SE", "SSE", "S", "SSW", "SW", "WSW", "W", "WNW", "NW", "NNW" };

            return arr[(val % 16)];
        }

        public static double CompassToDegrees(string heading)
        {
            var dict = new Dictionary<string, double>
            {
                { "N", 0 },
                {"NNE",22.5 },
                {"NE",45 },
                {"ENE",67.5 },
                {"E",90 },
                {"ESE",112.5 },
                {"SE",135 },
                {"SSE",157.5 },
                {"S",180 },
                {"SSW", 202.5 },
                {"SW",225 },
                {"WSW",247.5 },
                {"W",270 },
                {"WNW", 292.5 },
                {"NW",315 },
                {"NNW",337.5 },
                {"NORTH", 0 },
                {"EAST", 90 },
                {"SOUTH", 170 },
                {"WEST", 270 }
            };

          /*  var tree = new IntervalTree<double, string>()
            {
                { 0, 22.4, "N" },
                { 22.5, 39.9, "NNE" },
                { 40, 44.9, "NE" },
                { 45, 67.4, "ENE" },
                { 67.5, 89.9, "E" },
                { 90, 112.4, "ESE" },
                { 112.5, 134.9, "SE" },
                { 135, 157.4, "SSE" },
                { 157.5, 179.9, "S" },
                { 180, 202.4, "SSW" },
                { 202.5, 224.9, "SW" },
                { 225, 247.4, "WSW" },
                { 247.5, 269.9, "W" },
                { 270, 292.4, "WNW" },
                { 292.5, 314.9, "NW" },
                { 315, 337.4, "NNW" },
                {337.5, 360, "N" }
            };*/


            if (!dict.TryGetValue(heading.ToUpper(), out double result))
            {
                result = -1;
            }

            return result;
        }
    }
}
