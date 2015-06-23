
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
        [DataMember()]
        public int FlightId { get; set; }
        [DataMember()]
        public int BeaconId { get; set; }
        [DataMember()]
        public int ViewOrder { get; set; }
        #endregion

        #region " Methods "
        public void ReadFlightBeaconBase(FlightBeaconBase flightBeacon)
        {
            if (flightBeacon.FlightId > -1)
                FlightId = flightBeacon.FlightId;

            if (flightBeacon.BeaconId > -1)
                BeaconId = flightBeacon.BeaconId;

            if (flightBeacon.ViewOrder > -1)
                ViewOrder = flightBeacon.ViewOrder;

        }
        #endregion

    }
}



