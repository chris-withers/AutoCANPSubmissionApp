using AutoCANP.Api.Types;
using System;
using System.Collections.Generic;

namespace AutoCANP.Api.BusinessLogic.Utilities
{
    public static class Compass
    {
        public static string DegreesToCompass(double degrees)
        {
            string[] caridnals = { "N", "NNE", "NE", "ENE", "E", "ESE", "SE", "SSE", "S", "SSW", "SW", "WSW", "W", "WNW", "NW", "NNW", "N" };
            return caridnals[(int)Math.Round(((double)degrees * 10 % 3600) / 225)];
        }

        public static List<string> GetWindDirections(List<Value> data)
        {
            var windDirections = new List<string>();

            foreach (var item in data)
            {
                windDirections.Add(Compass.DegreesToCompass(item.value));
            }

            return windDirections;
        }
    }
}
