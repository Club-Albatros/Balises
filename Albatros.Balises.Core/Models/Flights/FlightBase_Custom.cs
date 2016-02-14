using Albatros.Balises.Core.Common;
using Albatros.Balises.Core.Repositories;

namespace Albatros.Balises.Core.Models.Flights
{
    public partial class FlightBase
    {

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

        public void RecalculateDistanceAndTime()
        {

            var flightTime = LandingTime.Subtract(TakeoffTime);
            DurationMins = (int)flightTime.TotalMinutes;

            // calculate distance
            var lastPoint = new Point() { Latitude = TakeoffLatitude, Longitude = TakeoffLongitude };
            Distance = 0;
            foreach (var passedBeacon in FlightBeaconRepository.Instance.GetFlightBeaconsByFlight(FlightId))
            {
                Distance += EarthCalculations.DistanceInMeters(lastPoint.Latitude, lastPoint.Longitude, passedBeacon.Latitude, passedBeacon.Longitude);
            }
            Distance += EarthCalculations.DistanceInMeters(lastPoint.Latitude, lastPoint.Longitude, LandingLatitude, LandingLongitude);

        }

    }
}



