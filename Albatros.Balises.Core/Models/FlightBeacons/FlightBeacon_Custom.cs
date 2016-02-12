using Albatros.Balises.Core.Models.Beacons;

namespace Albatros.Balises.Core.Models.FlightBeacons
{
    public partial class FlightBeacon  : FlightBeaconBase 
    {
        public static FlightBeacon FromBeacon(Beacon beacon)
        {
            return new FlightBeacon() { Altitude = beacon.Altitude, BeaconId = beacon.BeaconId, Code = beacon.Code, Coords = beacon.Coords, Description = beacon.Description, Latitude = beacon.Latitude, Longitude = beacon.Longitude, Name = beacon.Name, Region = beacon.Region, Points = beacon.Points };
        }
    }
}
