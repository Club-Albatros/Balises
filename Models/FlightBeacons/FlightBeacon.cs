
using System.Runtime.Serialization;
using Albatros.DNN.Modules.Balises.Models.Beacons;
using DotNetNuke.ComponentModel.DataAnnotations;

namespace Albatros.DNN.Modules.Balises.Models.FlightBeacons
{

    [TableName("vw_Albatros_Balises_FlightBeacons")]
    [DataContract]
    [Scope("FlightId")]
    public partial class FlightBeacon : FlightBeaconBase
    {

        #region " Private Members "
        #endregion

        #region " Constructors "
        public FlightBeacon()
            : base()
        {
        }
        #endregion

        #region " Public Properties "
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Coords { get; set; }
        [DataMember]
        public double Latitude { get; set; }
        [DataMember]
        public double Longitude { get; set; }
        [DataMember]
        public int Altitude { get; set; }
        [DataMember]
        public int Region { get; set; }
        [DataMember]
        public int Points { get; set; }
        #endregion

        #region " Public Methods "
        public FlightBeaconBase GetFlightBeaconBase()
        {
            FlightBeaconBase res = new FlightBeaconBase();
            res.FlightId = FlightId;
            res.BeaconId = BeaconId;
            res.PassageTime = PassageTime;
            res.PassedDistance = PassedDistance;
            return res;
        }

        public static FlightBeacon FromBeacon(Beacon beacon)
        {
            return new FlightBeacon() { Altitude = beacon.Altitude, BeaconId = beacon.BeaconId, Code = beacon.Code, Coords = beacon.Coords, Description = beacon.Description, Latitude = beacon.Latitude, Longitude = beacon.Longitude, Name = beacon.Name, Region = beacon.Region, Points = beacon.Points };
        }
        #endregion

    }
}
