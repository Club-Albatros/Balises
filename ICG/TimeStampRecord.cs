using System;

namespace Albatros.DNN.Modules.Balises.ICG
{
    public class TimeStampRecord : Record
    {

        public TimeStampRecord(string rec, DateTime flightdate)
            : base(rec)
        {
            Time = flightdate.Date;
            Time = Time.AddHours(Convert.ToInt32(rec.Substring(1, 2)));
            Time = Time.AddMinutes(Convert.ToInt32(rec.Substring(3, 2)));
            Time = Time.AddSeconds(Convert.ToInt32(rec.Substring(5, 2)));
        }

        public DateTime Time { get; private set; }
    }
}
