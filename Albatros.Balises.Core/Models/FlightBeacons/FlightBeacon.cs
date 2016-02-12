
using System.Runtime.Serialization;
using DotNetNuke.ComponentModel.DataAnnotations;

namespace Albatros.Balises.Core.Models.FlightBeacons
{

    [TableName("vw_Albatros_Balises_FlightBeacons")]
    [DataContract]
    public partial class FlightBeacon  : FlightBeaconBase 
    {

        #region .ctor
        public FlightBeacon()  : base() 
        {
        }
        #endregion

        #region Properties
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int Points { get; set; }
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
        #endregion

        #region Methods
        public FlightBeaconBase GetFlightBeaconBase()
        {
            FlightBeaconBase res = new FlightBeaconBase();
             res.FlightId = FlightId;
             res.BeaconId = BeaconId;
             res.PassageTime = PassageTime;
             res.PassedDistance = PassedDistance;
            return res;
        }
        #endregion

    }
}
