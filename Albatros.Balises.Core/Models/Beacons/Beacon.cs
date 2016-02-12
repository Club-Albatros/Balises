
using System.Runtime.Serialization;
using DotNetNuke.ComponentModel.DataAnnotations;

namespace Albatros.Balises.Core.Models.Beacons
{

    [TableName("vw_Albatros_Balises_Beacons")]
    [PrimaryKey("BeaconId", AutoIncrement = true)]
    [DataContract]
    [Scope("PortalId")]                
    public partial class Beacon  : BeaconBase 
    {

        #region .ctor
        public Beacon()  : base() 
        {
        }
        #endregion

        #region Properties
        [DataMember]
        public string CreatedByUser { get; set; }
        [DataMember]
        public string ModifiedByUser { get; set; }
        #endregion

        #region Methods
        public BeaconBase GetBeaconBase()
        {
            BeaconBase res = new BeaconBase();
             res.BeaconId = BeaconId;
             res.PortalId = PortalId;
             res.Code = Code;
             res.Name = Name;
             res.Description = Description;
             res.Coords = Coords;
             res.Latitude = Latitude;
             res.Longitude = Longitude;
             res.Altitude = Altitude;
             res.Region = Region;
             res.Points = Points;
  res.CreatedByUserID = CreatedByUserID;
  res.CreatedOnDate = CreatedOnDate;
  res.LastModifiedByUserID = LastModifiedByUserID;
  res.LastModifiedOnDate = LastModifiedOnDate;
            return res;
        }
        #endregion

    }
}
