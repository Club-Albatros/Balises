
using System.Runtime.Serialization;
using DotNetNuke.ComponentModel.DataAnnotations;

namespace Albatros.DNN.Modules.Balises.Models.Beacons
{

    [TableName("vw_Albatros_Balises_Beacons")]
    [PrimaryKey("BeaconId", AutoIncrement = true)]
    [DataContract]
    [Scope("ModuleId")]                
    public partial class Beacon  : BeaconBase 
    {

        #region " Private Members "
        #endregion

        #region " Constructors "
        public Beacon()  : base() 
        {
        }
        #endregion

        #region " Public Properties "
        [DataMember()]
        public string CreatedByUser { get; set; }
        [DataMember()]
        public string LastModifiedByUser { get; set; }
        #endregion

        #region " Public Methods "
        public BeaconBase GetBeaconBase()
        {
            BeaconBase res = new BeaconBase();
             res.BeaconId = BeaconId;
             res.ModuleId = ModuleId;
             res.Code = Code;
             res.Name = Name;
             res.Description = Description;
             res.Coords = Coords;
             res.Latitude = Latitude;
             res.Longitude = Longitude;
             res.Altitude = Altitude;
             res.Region = Region;
             res.Points = Points;
            return res;
        }
        #endregion

    }
}
