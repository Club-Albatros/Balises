using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Albatros.DNN.Modules.Balises.Controllers;
using Albatros.DNN.Modules.Balises.ICG;
using Albatros.DNN.Modules.Balises.Models.Beacons;
using DotNetNuke.Entities.Portals;

namespace Albatros.DNN.Modules.Balises.Common
{
    public class BalisesPath
    {

        public string FilePath { get; set; }
        public SortedDictionary<DateTime, Beacon> PassedBeacons { get; set; }

        public BalisesPath(int userId, string filePath, int maxDistance)
        {
            PassedBeacons = new SortedDictionary<DateTime, Beacon>();
            FilePath = filePath;
            var fullPath = string.Format("{0}\\Albatros\\Balises\\{1}\\{2}", PortalSettings.Current.HomeDirectoryMapPath, userId, filePath);
            ICGFile icg;
            using (StreamReader sr = new StreamReader(fullPath))
            {
                icg = new ICGFile(sr.ReadToEnd());

            }
            var beacons = BeaconsController.GetBeacons(PortalSettings.Current.PortalId);
            double distanceKm = (double)maxDistance / 1000;
            Beacon lastBeacon = null;
            foreach (var flightPoint in icg.BRecords.Values)
            {
                foreach (var beacon in beacons.Where(b => b.Code == "RDEC1"))
                {
                    if (
                        EarthCalculations.Distance(flightPoint.Latitude, flightPoint.Longitude, beacon.Latitude,
                            beacon.Longitude) <= distanceKm)
                    {
                        if (!PassedBeacons.ContainsKey(flightPoint.Time))
                        {
                            if (lastBeacon == null || lastBeacon != beacon)
                            {
                                PassedBeacons.Add(flightPoint.Time, beacon);
                                lastBeacon = beacon;
                            }
                        }
                    }
                }
            }

        }
    }
}