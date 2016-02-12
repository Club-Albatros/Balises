using Albatros.Balises.Core.Common;
using System;
namespace Albatros.Balises.Core.IGC
{
    /// <summary>
    /// Fix rec
    /// </summary>
    /// <remarks></remarks>
    public class BRecord : ExtendedRecord
    {
        public double Latitude { get; private set; }

        public double Longitude { get; private set; }

        public string FixValidity { get; private set; }

        public int PressureAltitude { get; private set; }

        public int GnssAltitude { get; private set; }

        public int Distance { get; private set; }

        public int TimeDifference { get; private set; }

        public float Speed { get; private set; }

        public int AltitudeDifference { get; private set; }

        public float Vario { get; private set; }

        public BRecord(string rec, DateTime flightdate, ExtensionRecord extensions)
            : base(rec, flightdate, extensions)
        {
            AltitudeDifference = 0;
            Speed = 0;
            TimeDifference = 1;
            Distance = 0;

            Latitude = Convert.ToInt32(rec.Substring(7, 2));
            Latitude = Latitude + ((Convert.ToDouble(rec.Substring(9, 2)) + Convert.ToDouble(rec.Substring(11, 3)) / 1000) / 60);
            if (rec.Substring(14, 1) == "S")
            {
                Latitude = Latitude * -1;
            }

            Longitude = Convert.ToInt32(rec.Substring(15, 3));
            Longitude = Longitude + ((Convert.ToDouble(rec.Substring(18, 2)) + Convert.ToDouble(rec.Substring(20, 3)) / 1000) / 60);
            if (rec.Substring(23, 1) == "W")
            {
                Longitude = Longitude * -1;
            }

            FixValidity = rec.Substring(24, 1);

            PressureAltitude = Convert.ToInt32(rec.Substring(25, 5));
            GnssAltitude = Convert.ToInt32(rec.Substring(30, 5));

        }


        public void CompareWith(BRecord lastRecord)
        {
            Distance = EarthCalculations.DistanceInMeters(lastRecord.Latitude, lastRecord.Longitude, Latitude, Longitude);
            TimeDifference = Convert.ToInt32(Time.Subtract(lastRecord.Time).TotalSeconds);
            Speed = (float)Distance / (float)TimeDifference;
            AltitudeDifference = PressureAltitude - lastRecord.PressureAltitude;
            Vario = Convert.ToSingle(AltitudeDifference / TimeDifference);
        }

    }
}
