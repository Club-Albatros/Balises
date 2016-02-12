using Albatros.Balises.Core.IGC;
using Albatros.Balises.Core.Models.Beacons;
using Albatros.Balises.Core.Models.FlightBeacons;
using Albatros.Balises.Core.Repositories;
using DotNetNuke.Entities.Portals;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;

namespace Albatros.Balises.Core.Common
{
    public class BalisesPath
    {
        [DataMember]
        public string FilePath { get; set; }
        [DataMember]
        public List<FlightBeacon> PassedBeacons { get; set; }
        [DataMember]
        public FlightBeacon TakeOff { get; set; }
        [DataMember]
        public FlightBeacon Landing { get; set; }

        public IgcFile Igc { get; set; }

        public BalisesPath(int userId, string filePath, int maxDistance)
        {
            PassedBeacons = new List<FlightBeacon>();
            FilePath = filePath;
            var fullPath = string.Format("{0}\\Albatros\\Balises\\{1}\\{2}", PortalSettings.Current.HomeDirectoryMapPath, userId, filePath);
            using (StreamReader sr = new StreamReader(fullPath))
            {
                Igc = new IgcFile(sr.ReadToEnd());

            }
            var beacons = BeaconRepository.Instance.GetBeacons(PortalSettings.Current.PortalId);

            // detect all flight beacons
            foreach (var flightPoint in Igc.BRecords.Values)
            {
                foreach (var beacon in beacons)
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

            // detect closest beacons for take off and landing
            TakeOff = new FlightBeacon() { BeaconId = -1, PassedDistance = 9999999, Description = "" };
            Landing = new FlightBeacon() { BeaconId = -1, PassedDistance = 9999999, Description = "" };
            foreach (var beacon in beacons)
            {
                var passedDistance = EarthCalculations.Distance(Igc.Takeoff.Latitude, Igc.Takeoff.Longitude, beacon.Latitude, beacon.Longitude) * 1000;
                if (passedDistance <= TakeOff.PassedDistance)
                {
                    TakeOff.BeaconId = beacon.BeaconId;
                    TakeOff.PassedDistance = (int)passedDistance;
                    TakeOff.Description = beacon.Name;
                }
                passedDistance = EarthCalculations.Distance(Igc.Landing.Latitude, Igc.Landing.Longitude, beacon.Latitude, beacon.Longitude) * 1000;
                if (passedDistance <= Landing.PassedDistance)
                {
                    Landing.BeaconId = beacon.BeaconId;
                    Landing.PassedDistance = (int)passedDistance;
                    Landing.Description = beacon.Name;
                }
            }

        }
    }
}
