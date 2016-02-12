using Albatros.Balises.Core.IGC;

namespace Albatros.Balises.Core.Common
{
    public class Point
	{

		public double Latitude;
		public double Longitude;

		public int Altitude;
		public Point(BRecord brec)
		{
			Latitude = brec.Latitude;
			Longitude = brec.Longitude;
			Altitude = brec.GnssAltitude;
		}

	}
}
