using System;

namespace Albatros.Balises.Core.Common
{
    public class EarthCalculations
    {

        public const int EarthRadiusMeters = 6371000;
        public static int DistanceInMeters(double lat1, double lon1, double lat2, double lon2)
        {
            double dLat = DegToRad(lat2 - lat1);
            double dLon = DegToRad(lon2 - lon1);
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos(DegToRad(lat1)) * Math.Cos(DegToRad(lat2)) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return (int)(EarthRadiusMeters * c);
        }

        public static double RadToDeg(double rad)
        {
            return rad * 180.0 / Math.PI;
        }

        public static double DegToRad(double deg)
        {
            return deg * Math.PI / 180.0;
        }

    }
}
