
using System.Runtime.Serialization;
using DotNetNuke.ComponentModel.DataAnnotations;

namespace Albatros.DNN.Modules.Balises.Models.FlightBeacons
{
    [TableName("Albatros_Balises_FlightBeacons")]
    [DataContract]
    [Scope("FlightId")]
    public partial class FlightBeaconBase
    {

        #region " Public Properties "
        [DataMember]
        public int FlightId { get; set; }
        [DataMember]
        public int BeaconId { get; set; }
        [DataMember]
        public System.DateTime PassageTime { get; set; }
        [DataMember]
        public int? PassedDistance { get; set; }
        #endregion

        #region " Methods "
        public void ReadFlightBeaconBase(FlightBeaconBase flightBeacon)
        {
            if (flightBeacon.FlightId > -1)
                FlightId = flightBeacon.FlightId;

            if (flightBeacon.BeaconId > -1)
                BeaconId = flightBeacon.BeaconId;

            PassageTime = flightBeacon.PassageTime;

            if (flightBeacon.PassedDistance > -1)
                PassedDistance = flightBeacon.PassedDistance;

        }
        #endregion

    }
}



