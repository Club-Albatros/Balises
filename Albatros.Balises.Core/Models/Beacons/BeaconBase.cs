
using System;
using System.Runtime.Serialization;
using DotNetNuke.ComponentModel.DataAnnotations;
using Albatros.Balises.Core.Common;
using Albatros.Balises.Core.Data;

namespace Albatros.Balises.Core.Models.Beacons
{
    [TableName("Albatros_Balises_Beacons")]
    [PrimaryKey("BeaconId", AutoIncrement = true)]
    [DataContract]
    [Scope("PortalId")]
    public partial class BeaconBase  : AuditableEntity 
    {

        #region .ctor
        public BeaconBase()
        {
            BeaconId = -1;
        }
        #endregion

        #region Properties
        [DataMember]
        public int BeaconId { get; set; }
        [DataMember]
        public int PortalId { get; set; }
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

        #region Methods
        public void ReadBeaconBase(BeaconBase beacon)
        {
            if (beacon.BeaconId > -1)
                BeaconId = beacon.BeaconId;

            if (beacon.PortalId > -1)
                PortalId = beacon.PortalId;

            if (!String.IsNullOrEmpty(beacon.Code))
                Code = beacon.Code;

            if (!String.IsNullOrEmpty(beacon.Name))
                Name = beacon.Name;

            if (!String.IsNullOrEmpty(beacon.Description))
                Description = beacon.Description;

            if (!String.IsNullOrEmpty(beacon.Coords))
                Coords = beacon.Coords;

            Latitude = beacon.Latitude;

            Longitude = beacon.Longitude;

            if (beacon.Altitude > -1)
                Altitude = beacon.Altitude;

            if (beacon.Region > -1)
                Region = beacon.Region;

            if (beacon.Points > -1)
                Points = beacon.Points;

        }
        #endregion

    }
}



