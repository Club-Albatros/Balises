using Albatros.Balises.Core.IGC;

namespace Albatros.Balises.Core.Common
{
    public class Point
    {

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Altitude { get; set; } = 0;

        public Point() { }
        public Point(BRecord brec)
        {
            Latitude = brec.Latitude;
            Longitude = brec.Longitude;
            Altitude = brec.GnssAltitude;
        }

    }
}
