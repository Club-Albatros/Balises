using System.Linq;
using Albatros.Balises.Core.Common;
using Albatros.Balises.Core.Repositories;
using DotNetNuke.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Albatros.Balises.Core.Models.FlightBeacons;

namespace Albatros.Balises.Core.Models.Flights
{
    public partial class FlightBase
    {

        [IgnoreColumn]
        public string BeaconList { get; set; }

        public void ReadLandingCoordinates(string coordinates)
        {
            var pt = coordinates.ToPoint();
            LandingLatitude = pt.Latitude;
            LandingLongitude = pt.Longitude;
            LandingCoords = pt.ToSwissCoordinates();
        }

        public void ReadTakeoffCoordinates(string coordinates)
        {
            var pt = coordinates.ToPoint();
            TakeoffLatitude = pt.Latitude;
            TakeoffLongitude = pt.Longitude;
            TakeoffCoords = pt.ToSwissCoordinates();
        }

        public void CheckLandingBeacon(int maxDistance)
        {
            LandingBeaconId = -1;
            var beacons = BeaconRepository.Instance.GetClosestBeacon(PortalId, LandingLatitude, LandingLongitude, maxDistance);
            if (beacons.Count() > 0)
            {
                if (beacons.First().Code.Contains("ATT"))
                {
                    LandingBeaconId = beacons.First().BeaconId;
                }
            }
        }

        public void RecalculateFlightTime()
        {
            var flightTime = LandingTime.Subtract(TakeoffTime);
            DurationMins = (int)flightTime.TotalMinutes;
        }

        public void RecalculateTotals()
        {
            RecalculateTotals(FlightBeaconRepository.Instance.GetFlightBeaconsByFlight(FlightId));
        }

        public void RecalculateTotals(IEnumerable<FlightBeacon> passedBeacons)
        {

            RecalculateFlightTime();

            // calculate distance
            var lastPoint = new Point() { Latitude = TakeoffLatitude, Longitude = TakeoffLongitude };
            Distance = 0;
            foreach (var passedBeacon in passedBeacons)
            {
                Distance += EarthCalculations.DistanceInMeters(lastPoint.Latitude, lastPoint.Longitude, passedBeacon.Latitude, passedBeacon.Longitude);
            }
            Distance += EarthCalculations.DistanceInMeters(lastPoint.Latitude, lastPoint.Longitude, LandingLatitude, LandingLongitude);

            // calculate points
            TotalPoints = passedBeacons.Sum(fb => fb.Points);
            if (Category == 0) { TotalPoints = (int)(0.5 + TotalPoints * 1.3); } // 1.3 coeff for para
            if (LandingBeaconId != -1) { TotalPoints = (int)(0.5 + TotalPoints * 1.2); } // 1.2 for landing in a designated spot

        }

        [IgnoreColumn]
        public IEnumerable<FlightBeacon> PassedBeacons
        {
            get {
                return FlightBeaconRepository.Instance.GetFlightBeaconsByFlight(FlightId).OrderBy(fb => fb.PassageTime);

            }
        }

    }
}



