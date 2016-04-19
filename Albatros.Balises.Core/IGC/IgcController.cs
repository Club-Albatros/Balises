using Albatros.Balises.Core.Common;
using Albatros.Balises.Core.Models.FlightBeacons;
using Albatros.Balises.Core.Models.Flights;
using Albatros.Balises.Core.Repositories;
using DotNetNuke.Entities.Portals;

namespace Albatros.Balises.Core.IGC
{
    public class IgcController
    {
        public static Flight AddFlightToUser(int portalId, int userId, string igcText, int beaconPassDistanceMeters)
        {
            var path = new BalisesPath(portalId, userId, new IgcFile(igcText), beaconPassDistanceMeters);
            var flight = FlightRepository.Instance.FindFlight(portalId, userId, path.Igc.DetectedStart);
            if (flight == null)
            {
                var f = new FlightBase()
                {
                    EntryMethod = 1,
                    TakeoffTime = path.Igc.DetectedStart,
                    DurationMins = (int)path.Igc.FlightTime.TotalMinutes,
                    Distance = path.OfficialDistance,
                    MaxHeight = path.Igc.MaxAltitude,
                    MaxVario = path.Igc.MaxVario,
                    MaxSpeed = path.Igc.MaxSpeed,
                    AverageSpeed = path.Igc.AverageSpeed,
                    LandingCoords = path.Igc.Landing.ToSwissCoordinates(),
                    LandingDescription = path.Landing.Description,
                    LandingLatitude = path.Igc.Landing.Latitude,
                    LandingLongitude = path.Igc.Landing.Longitude,
                    LandingAltitude = path.Igc.Landing.Altitude,
                    LandingTime = path.Igc.DetectedLandingTime,
                    LandingBeaconId = (path.Landing.Code.Contains("ATT") ? path.Landing.BeaconId : -1),
                    PortalId = portalId,
                    UserId = userId,
                    TakeoffCoords = path.Igc.Takeoff.ToSwissCoordinates(),
                    TakeoffDescription = path.TakeOff.Description,
                    TakeoffLatitude = path.Igc.Takeoff.Latitude,
                    TakeoffLongitude = path.Igc.Takeoff.Longitude,
                    TakeoffAltitude = path.Igc.Takeoff.Altitude,
                    Summary = path.Igc.Report(),
                    Status = (path.PassedBeacons.Count == 0 ? 3 : 0),
                    ValidatedOnDate = new System.DateTime(1900, 1, 1),
                    Category = (path.Igc.GliderType.ToUpper() == "PARA" ? 0 : 1),
                    TotalPoints = 0
                };
                f.RecalculateTotals(path.PassedBeacons);
                FlightRepository.Instance.AddFlight(ref f, userId);
                foreach (var pt in path.PassedBeacons)
                {
                    var fb = new FlightBeaconBase()
                    {
                        FlightId = f.FlightId,
                        BeaconId = pt.BeaconId,
                        PassedDistance = pt.PassedDistance,
                        PassOrder = pt.PassOrder
                    };
                    FlightBeaconRepository.Instance.AddFlightBeacon(fb);
                }
                var fullPath = string.Format("{0}\\Albatros\\Balises\\{1}", PortalSettings.Current.HomeDirectoryMapPath, userId);
                if (!System.IO.Directory.Exists(fullPath))
                {
                    System.IO.Directory.CreateDirectory(fullPath);
                }
                fullPath += "\\" + string.Format("path-{0}.igc", f.FlightId);
                using (var s = new System.IO.StreamWriter(fullPath))
                {
                    s.Write(igcText);
                }
                flight = FlightRepository.Instance.GetFlight(portalId, f.FlightId);
            }
            return flight;
        }
    }
}
