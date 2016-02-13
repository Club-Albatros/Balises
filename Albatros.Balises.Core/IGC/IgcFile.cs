using Albatros.Balises.Core.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Albatros.Balises.Core.IGC
{
    public class IgcFile
    {
        public string Contents { get; set; }

        public ARecord ARecord { get; private set; }
        public SortedDictionary<DateTime, BRecord> BRecords { get; private set; }
        public List<ERecord> ERecords { get; private set; }
        public string GRecord { get; set; }
        public Dictionary<string, HRecord> HRecords { get; set; }
        public ExtensionRecord IRecord { get; private set; }
        public ExtensionRecord JRecord { get; private set; }

        public DateTime FlightDate { get; private set; }
        public string PilotName { get; private set; }
        public string GliderType { get; private set; }
        public string GliderRegistration { get; private set; }
        public string GliderClass { get; private set; }

        public DateTime DetectedStart { get; private set; }
        public DateTime DetectedLandingTime { get; private set; }
        public TimeSpan FlightTime { get; private set; }
        public Common.Point Takeoff { get; private set; }
        public Common.Point Landing { get; private set; }
        public int MaxAltitude { get; private set; }
        public int MinAltitude { get; private set; }
        public float MaxVario { get; private set; }
        public float MinVario { get; private set; }
        public float MaxSpeed { get; private set; }
        public int Distance { get; private set; }
        public float AverageSpeed { get; private set; }
        
        public IgcFile(string contents)
        {
            BRecords = new SortedDictionary<DateTime, BRecord>();
            ERecords = new List<ERecord>();
            GRecord = "";
            HRecords = new Dictionary<string, HRecord>();
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
        public IgcFile(Stream contents)
        {
            BRecords = new SortedDictionary<DateTime, BRecord>();
            ERecords = new List<ERecord>();
            GRecord = "";
            HRecords = new Dictionary<string, HRecord>();
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
                            ARecord = new ARecord(l);
                            break;
                        case "B":
                            BRecord b = new BRecord(l, FlightDate, IRecord);
                            if (!BRecords.ContainsKey(b.Time))
                            {
                                BRecords.Add(b.Time, b);
                            }
                            break;
                        case "C":
                            break;
                        case "D":
                            break;
                        case "E":
                            ERecord e = new ERecord(l, FlightDate);
                            ERecords.Add(e);
                            if (e.Code == "STA")
                            {
                                DetectedStart = e.Time;
                            }
                            break;
                        case "F":
                            break;
                        case "G":
                            GRecord = GRecord + l.Substring(1);
                            break;
                        case "H":
                            HRecord h = new HRecord(l);
                            HRecords.Add(h.Code, h);
                            if (HRecords.ContainsKey("DTE"))
                            {
                                FlightDate = new DateTime(2000 + int.Parse(HRecords["DTE"].Value.Substring(4, 2)), int.Parse(HRecords["DTE"].Value.Substring(2, 2)), int.Parse(HRecords["DTE"].Value.Substring(0, 2)));
                            }
                            break;
                        case "I":
                            IRecord = new ExtensionRecord(l);
                            break;
                        case "J":
                            JRecord = new ExtensionRecord(l);
                            break;
                    }
                }
            }

            if (DetectedStart == null && BRecords.Count > 0)
            {
                DetectedStart = BRecords.First().Key;
            }

            // Process other H records
            if (HRecords.ContainsKey("PLT"))
            {
                PilotName = HRecords["PLT"].Value.CutOffUntilCharacter(":").Trim();
            }
            if (HRecords.ContainsKey("GTY"))
            {
                GliderType = HRecords["GTY"].Value.CutOffUntilCharacter(":").Trim();
            }
            if (HRecords.ContainsKey("GID"))
            {
                GliderRegistration = HRecords["GID"].Value.CutOffUntilCharacter(":").Trim();
            }
            if (HRecords.ContainsKey("CCL"))
            {
                GliderClass = HRecords["CCL"].Value.CutOffUntilCharacter(":").Trim();
            }

            // Process B records
            BRecord currentRecord = null;
            foreach (DateTime k in BRecords.Keys)
            {
                if (k > DetectedStart & currentRecord != null)
                {
                    if (Takeoff == null)
                    {
                        Takeoff = new Common.Point(currentRecord);
                    }
                    BRecords[k].CompareWith(currentRecord);
                    if (BRecords[k].GnssAltitude > MaxAltitude)
                    {
                        MaxAltitude = BRecords[k].GnssAltitude;
                    }
                    if (BRecords[k].GnssAltitude < MinAltitude)
                    {
                        MinAltitude = BRecords[k].GnssAltitude;
                    }
                    if (BRecords[k].Vario > MaxVario)
                    {
                        MaxVario = BRecords[k].Vario;
                    }
                    if (BRecords[k].Vario < MinVario)
                    {
                        MinVario = BRecords[k].Vario;
                    }
                    if (BRecords[k].Speed > MaxSpeed)
                    {
                        MaxSpeed = BRecords[k].Speed;
                    }
                    Distance = Distance + BRecords[k].Distance;
                }
                currentRecord = BRecords[k];
            }
            DetectedLandingTime = currentRecord.Time;
            FlightTime = DetectedLandingTime.Subtract(DetectedStart);
            Landing = new Common.Point(currentRecord);
            AverageSpeed = Convert.ToSingle((float)Distance / (float)FlightTime.TotalSeconds);

        }

        public string Report()
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Date: {0:d}\r\n", FlightDate);
            sb.AppendFormat("Start: {0:HH:MM:ss}\r\n", DetectedStart);
            sb.AppendFormat("Distance (km): {0}\r\n", Distance / 1000);
            sb.AppendFormat("Duration (mins): {0}\r\n", FlightTime.TotalMinutes);
            sb.AppendFormat("Avg Speed (m/s): {0}\r\n", AverageSpeed);
            sb.AppendFormat("Max Speed (m/s): {0}\r\n", MaxSpeed);
            sb.AppendFormat("Max Altitude (m): {0}\r\n", MaxAltitude);
            sb.AppendFormat("Max Vario (m/s): {0}\r\n", MaxVario);
            sb.AppendFormat("Min Vario (m/s): {0}\r\n", MinVario);

            return sb.ToString();

        }

    }
}
