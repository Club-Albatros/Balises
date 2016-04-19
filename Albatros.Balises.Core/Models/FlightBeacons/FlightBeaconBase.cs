
using System;
using System.Runtime.Serialization;
using DotNetNuke.ComponentModel.DataAnnotations;
using Albatros.Balises.Core.Common;
using Albatros.Balises.Core.Data;

namespace Albatros.Balises.Core.Models.FlightBeacons
{
    [TableName("Albatros_Balises_FlightBeacons")]
    [DataContract]
    public partial class FlightBeaconBase     {

        #region .ctor
        public FlightBeaconBase()
        {
        }
        #endregion

        #region Properties
        [DataMember]
        public int FlightId { get; set; }
        [DataMember]
        public int BeaconId { get; set; }
        [DataMember]
        public int? PassedDistance { get; set; }
        [DataMember]
        public int PassOrder { get; set; }
        #endregion

        #region Methods
        public void ReadFlightBeaconBase(FlightBeaconBase flightBeacon)
        {
            if (flightBeacon.FlightId > -1)
                FlightId = flightBeacon.FlightId;

            if (flightBeacon.BeaconId > -1)
                BeaconId = flightBeacon.BeaconId;

            if (flightBeacon.PassedDistance > -1)
                PassedDistance = flightBeacon.PassedDistance;

            if (flightBeacon.PassOrder > -1)
                PassOrder = flightBeacon.PassOrder;

        }
        #endregion

    }
}



