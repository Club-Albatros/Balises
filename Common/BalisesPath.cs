using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using Albatros.DNN.Modules.Balises.Controllers;
using Albatros.DNN.Modules.Balises.IGC;
using Albatros.DNN.Modules.Balises.Models.Beacons;
using Albatros.DNN.Modules.Balises.Models.FlightBeacons;
using DotNetNuke.Entities.Portals;

namespace Albatros.DNN.Modules.Balises.Common
{
    [DataContract]
    public class BalisesPath
    {
        [DataMember]
        public string FilePath { get; set; }
        [DataMember]
        public List<FlightBeacon> PassedBeacons { get; set; }

        public BalisesPath(int userId, string filePath, int maxDistance)
        {
            PassedBeacons = new List<FlightBeacon>();
            FilePath = filePath;
            var fullPath = string.Format("{0}\\Albatros\\Balises\\{1}\\{2}", PortalSettings.Current.HomeDirectoryMapPath, userId, filePath);
            IgcFile igc;
            using (StreamReader sr = new StreamReader(fullPath))
            {
                igc = new IgcFile(sr.ReadToEnd());

            }
            var beacons = BeaconsController.GetBeacons(PortalSettings.Current.PortalId);
            foreach (var flightPoint in igc.BRecords.Values)
            {
                foreach (var beacon in beacons.Where(b => b.Code == "RDEC1"))
                {
                    var passedDistance = EarthCalculations.Distance(flightPoint.Latitude, flightPoint.Longitude, beacon.Latitude, beacon.Longitude) * 1000;
                    if (passedDistance <= maxDistance)
                    {
                        PassedBeacons.RemoveAll(b => b.Code == beacon.Code && b.PassedDistance < passedDistance);
                        if (PassedBeacons.Count(b => b.Code == beacon.Code) == 0)
                        {
                            var b = FlightBeacon.FromBeacon(beacon);
                            b.PassageTime = flightPoint.Time;
                            b.PassedDistance = (int)passedDistance;
                            PassedBeacons.Add(b);
                        }
                    }
                }
            }

        }
    }
}