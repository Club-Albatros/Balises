namespace Albatros.DNN.Modules.Balises.Common
{
	public class Point
	{

		public double Latitude;
		public double Longitude;

		public int Altitude;
		public Point(ICG.BRecord brec)
		{
			Latitude = brec.Latitude;
			Longitude = brec.Longitude;
			Altitude = brec.GnssAltitude;
		}

	}
}
