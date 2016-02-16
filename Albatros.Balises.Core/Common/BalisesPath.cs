using Albatros.Balises.Core.IGC;
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
        public List<FlightBeacon> PassedBeacons { get; set; }
        [DataMember]
        public FlightBeacon TakeOff { get; set; }
        [DataMember]
        public FlightBeacon Landing { get; set; }

        public IgcFile Igc { get; set; }
        public int MaxDistance { get; set; }
        public int OfficialDistance { get; set; } = 0;

        public BalisesPath(int userId, string filePath, int maxDistance)
        {
            MaxDistance = maxDistance;
            var fullPath = string.Format("{0}\\Albatros\\Balises\\{1}\\{2}", PortalSettings.Current.HomeDirectoryMapPath, userId, filePath);
            using (StreamReader sr = new StreamReader(fullPath))
            {
                Igc = new IgcFile(sr.ReadToEnd());
            }
            process();
        }
        public BalisesPath(int userId, IgcFile file, int maxDistance)
        {
            Igc = file;
            MaxDistance = maxDistance;
            process();
        }
        private void process()
        {
            PassedBeacons = new List<FlightBeacon>();
            var beacons = BeaconRepository.Instance.GetBeacons(PortalSettings.Current.PortalId);

            // detect all flight beacons
            foreach (var flightPoint in Igc.BRecords.Values)
            {
                foreach (var beacon in beacons)
                {
                    var passedDistance = EarthCalculations.DistanceInMeters(flightPoint.Latitude, flightPoint.Longitude, beacon.Latitude, beacon.Longitude);
                    if (passedDistance <= MaxDistance)
                    {
                        if (PassedBeacons.Count() > 0 && PassedBeacons.Last().Code == beacon.Code)
                        {
                            if (PassedBeacons.Last().PassedDistance > passedDistance)
                            {
                                PassedBeacons.Last().PassageTime = flightPoint.Time;
                                PassedBeacons.Last().PassedDistance = passedDistance;
                            }
                        }
                        else
                        {
                            var b = FlightBeacon.FromBeacon(beacon);
                            b.PassageTime = flightPoint.Time;
                            b.PassedDistance = passedDistance;
                            PassedBeacons.Add(b);
                        }
                    }
                }
            }

            // detect closest beacons for take off and landing
            TakeOff = new FlightBeacon() { BeaconId = -1, PassedDistance = 3 * MaxDistance, Description = "", Latitude = Igc.Takeoff.Latitude, Longitude = Igc.Takeoff.Longitude, Altitude = Igc.Takeoff.Altitude };
            Landing = new FlightBeacon() { BeaconId = -1, PassedDistance = 3 * MaxDistance, Description = "", Latitude = Igc.Landing.Latitude, Longitude = Igc.Landing.Longitude, Altitude = Igc.Landing.Altitude };
            foreach (var beacon in beacons)
            {
                var passedDistance = EarthCalculations.DistanceInMeters(Igc.Takeoff.Latitude, Igc.Takeoff.Longitude, beacon.Latitude, beacon.Longitude);
                if (passedDistance <= TakeOff.PassedDistance)
                {
                    TakeOff.BeaconId = beacon.BeaconId;
                    TakeOff.PassedDistance = passedDistance;
                    TakeOff.Description = beacon.Name;
                }
                passedDistance = EarthCalculations.DistanceInMeters(Igc.Landing.Latitude, Igc.Landing.Longitude, beacon.Latitude, beacon.Longitude);
                if (passedDistance <= Landing.PassedDistance)
                {
                    Landing.BeaconId = beacon.BeaconId;
                    Landing.PassedDistance = passedDistance;
                    Landing.Description = beacon.Name;
                }
            }

            // calculate distance
            var lastPoint = Igc.Takeoff;
            foreach (var passedBeacon in PassedBeacons)
            {
                OfficialDistance += EarthCalculations.DistanceInMeters(lastPoint.Latitude, lastPoint.Longitude, passedBeacon.Latitude, passedBeacon.Longitude);
            }
            OfficialDistance += EarthCalculations.DistanceInMeters(lastPoint.Latitude, lastPoint.Longitude, Igc.Landing.Latitude, Igc.Landing.Longitude);

        }
    }
}
