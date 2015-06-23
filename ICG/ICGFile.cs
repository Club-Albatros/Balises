using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Albatros.DNN.Modules.Balises.ICG
{
    public class ICGFile
    {
        public string Contents { get; set; }
        public DateTime FlightDate { get; private set; }
        private ARecord _aRecord;
        private SortedDictionary<DateTime, BRecord> _bRecords = new SortedDictionary<DateTime, BRecord>();
        private List<ERecord> _eRecords = new List<ERecord>();
        private ExtensionRecord _iRecord;
        private ExtensionRecord _jRecord;
        private string _gRecord = "";
        private Dictionary<string, HRecord> _hRecords = new Dictionary<string, HRecord>();
        public DateTime DetectedStart { get; private set; }
        public TimeSpan FlightTime { get; private set; }
        public Common.Point Takeoff { get; private set; }
        public Common.Point Landing { get; private set; }
        public int MaxAltitude { get; private set; }
        public int MinAltitude { get; private set; }
        public float MaxVario { get; private set; }
        public float MinVario { get; private set; }
        public float MaxSpeed { get; private set; }
        public float Distance { get; private set; }
        public float AverageSpeed { get; private set; }

        public ICGFile(string contents)
        {
            Takeoff = null;
            Landing = null;
            MaxAltitude = 0;
            MinAltitude = 0;
            MaxVario = 0;
            MinVario = 0;
            MaxSpeed = 0;
            Distance = 0;
            AverageSpeed = 0;
            Contents = contents;
            ReadContents();
        }
        public ICGFile(Stream contents)
        {
            Takeoff = null;
            Landing = null;
            MaxAltitude = 0;
            MinAltitude = 0;
            MaxVario = 0;
            MinVario = 0;
            MaxSpeed = 0;
            Distance = 0;
            AverageSpeed = 0;
            using (StreamReader tr = new StreamReader(contents))
            {
                Contents = tr.ReadToEnd();
            }
            ReadContents();
        }


        private void ReadContents()
        {
            using (StringReader sr = new StringReader(Contents))
            {
                while (sr.Peek() >= 0)
                {
                    string l = sr.ReadLine();
                    switch (l.Substring(0, 1))
                    {
                        case "A":
                            _aRecord = new ARecord(l);
                            break;
                        case "B":
                            BRecord b = new BRecord(l, FlightDate, _iRecord);
                            if (!_bRecords.ContainsKey(b.Time))
                            {
                                _bRecords.Add(b.Time, b);
                            }
                            break;
                        case "C":
                            break;
                        case "D":
                            break;
                        case "E":
                            ERecord e = new ERecord(l, FlightDate);
                            _eRecords.Add(e);
                            if (e.Code == "STA")
                            {
                                DetectedStart = e.Time;
                            }
                            break;
                        case "F":
                            break;
                        case "G":
                            _gRecord = _gRecord + l.Substring(1);
                            break;
                        case "H":
                            HRecord h = new HRecord(l);
                            _hRecords.Add(h.Code, h);
                            break;
                        case "I":
                            _iRecord = new ExtensionRecord(l);
                            break;
                        case "J":
                            _jRecord = new ExtensionRecord(l);
                            break;
                    }
                }
            }

            // Process B records
            BRecord currentRecord = null;
            foreach (DateTime k in _bRecords.Keys)
            {
                if (k > DetectedStart & currentRecord != null)
                {
                    if (Takeoff == null)
                    {
                        Takeoff = new Common.Point(currentRecord);
                    }
                    _bRecords[k].CompareWith(currentRecord);
                    if (_bRecords[k].GnssAltitude > MaxAltitude)
                    {
                        MaxAltitude = _bRecords[k].GnssAltitude;
                    }
                    if (_bRecords[k].GnssAltitude < MinAltitude)
                    {
                        MinAltitude = _bRecords[k].GnssAltitude;
                    }
                    if (_bRecords[k].Vario > MaxVario)
                    {
                        MaxVario = _bRecords[k].Vario;
                    }
                    if (_bRecords[k].Vario < MinVario)
                    {
                        MinVario = _bRecords[k].Vario;
                    }
                    if (_bRecords[k].Speed > MaxSpeed)
                    {
                        MaxSpeed = _bRecords[k].Speed;
                    }
                    Distance = Distance + _bRecords[k].Distance;
                }
                currentRecord = _bRecords[k];
            }
            FlightTime = currentRecord.Time.Subtract(DetectedStart);
            Landing = new Common.Point(currentRecord);
            AverageSpeed = Convert.ToSingle(Distance / FlightTime.TotalSeconds);

        }

        public string Report()
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Date: {0:d}<br />", FlightDate);
            sb.AppendFormat("Start: {0:HH:MM:ss}<br />", DetectedStart);
            sb.AppendFormat("Distance (km): {0}<br />", Distance / 1000);
            sb.AppendFormat("Duration (mins): {0}<br />", FlightTime.TotalMinutes);
            sb.AppendFormat("Avg Speed (m/s): {0}<br />", AverageSpeed);
            sb.AppendFormat("Max Speed (m/s): {0}<br />", MaxSpeed);
            sb.AppendFormat("Max Altitude (m): {0}<br />", MaxAltitude);
            sb.AppendFormat("Max Vario (m/s): {0}<br />", MaxVario);
            sb.AppendFormat("Min Vario (m/s): {0}<br />", MinVario);

            return sb.ToString();

        }

    }
}
