using Albatros.Balises.Core.Models.Beacons;
using Albatros.Balises.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Albatros.DNN.Modules.Balises.Common
{
    public class CompeGpsFile
    {
        public string Content { get; set; }
        private StringBuilder writer { get; set; }

        public CompeGpsFile(int portalId)
        {
            writer = new StringBuilder();
            AddLine("G", "WGS 84");
            AddLine("U", "1");
            foreach (var beacon in BeaconRepository.Instance.GetBeacons(portalId))
            {
                AddBeacon(beacon);
            }
            Content = writer.ToString();
        }

        public void AddBeacon(Beacon b)
        {
            var ns = (b.Latitude > 0) ? "N" : "S";
            var ew = (b.Longitude > 0) ? "E" : "W";
            var line = string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0} A {1}º{2} {3}º{4} {5:dd-MMM-yyyy HH:mm:ss} {6} {7}", b.Code, Math.Abs(b.Latitude), ns, Math.Abs(b.Longitude), ew, DateTime.Now, b.Altitude, b.Name.ToUpper());
            AddLine("W", line);
            AddLine("w", "Red Diamond,0,-1.0,16316664,248,0,39,,0.0,,-1,0");
        }

        public void AddLine(string lineType, string line)
        {
            writer.AppendFormat("{0} {1}\r\n", lineType, line);
        }
    }
}