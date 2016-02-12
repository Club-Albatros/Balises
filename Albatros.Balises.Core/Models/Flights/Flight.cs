
using System.Runtime.Serialization;
using DotNetNuke.ComponentModel.DataAnnotations;

namespace Albatros.Balises.Core.Models.Flights
{

    [TableName("vw_Albatros_Balises_Flights")]
    [PrimaryKey("FlightId", AutoIncrement = true)]
    [DataContract]
    [Scope("PortalId")]                
    public partial class Flight  : FlightBase 
    {

        #region .ctor
        public Flight()  : base() 
        {
        }
        #endregion

        #region Properties
        [DataMember]
        public int? NrBeacons { get; set; }
        [DataMember]
        public int? TotalPoints { get; set; }
        [DataMember]
        public int? NrComments { get; set; }
        [DataMember]
        public string CreatedByUser { get; set; }
        [DataMember]
        public string ModifiedByUser { get; set; }
        [DataMember]
        public string ValidatedByUser { get; set; }
        #endregion

        #region Methods
        public FlightBase GetFlightBase()
        {
            FlightBase res = new FlightBase();
             res.FlightId = FlightId;
             res.PortalId = PortalId;
             res.FlightStart = FlightStart;
             res.DurationMins = DurationMins;
             res.Distance = Distance;
             res.Category = Category;
             res.StartDescription = StartDescription;
             res.StartCoords = StartCoords;
             res.StartLatitude = StartLatitude;
             res.StartLongitude = StartLongitude;
             res.LandingDescription = LandingDescription;
             res.LandingTime = LandingTime;
             res.LandingCoords = LandingCoords;
             res.LandingLatitude = LandingLatitude;
             res.LandingLongitude = LandingLongitude;
             res.Summary = Summary;
             res.Validated = Validated;
             res.Rejected = Rejected;
             res.ValidatedByUserID = ValidatedByUserID;
             res.ValidatedOnDate = ValidatedOnDate;
  res.CreatedByUserID = CreatedByUserID;
  res.CreatedOnDate = CreatedOnDate;
  res.LastModifiedByUserID = LastModifiedByUserID;
  res.LastModifiedOnDate = LastModifiedOnDate;
            return res;
        }
        #endregion

    }
}
