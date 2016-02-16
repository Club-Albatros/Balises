
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
        public int TotalPoints { get; set; }
        [DataMember]
        public int? NrComments { get; set; }
        [DataMember]
        public string Pilot { get; set; }
        [DataMember]
        public string CreatedByUser { get; set; }
        [DataMember]
        public string LastModifiedByUser { get; set; }
        [DataMember]
        public string ValidatedByUser { get; set; }
        #endregion

        #region Methods
        public FlightBase GetFlightBase()
        {
            FlightBase res = new FlightBase();
             res.FlightId = FlightId;
             res.PortalId = PortalId;
             res.UserId = UserId;
             res.TakeoffDescription = TakeoffDescription;
             res.TakeoffTime = TakeoffTime;
             res.TakeoffCoords = TakeoffCoords;
             res.TakeoffLatitude = TakeoffLatitude;
             res.TakeoffLongitude = TakeoffLongitude;
             res.TakeoffAltitude = TakeoffAltitude;
             res.LandingDescription = LandingDescription;
             res.LandingTime = LandingTime;
             res.LandingCoords = LandingCoords;
             res.LandingLatitude = LandingLatitude;
             res.LandingLongitude = LandingLongitude;
             res.LandingAltitude = LandingAltitude;
             res.MaxHeight = MaxHeight;
             res.MaxVario = MaxVario;
             res.MaxSpeed = MaxSpeed;
             res.AverageSpeed = AverageSpeed;
             res.DurationMins = DurationMins;
             res.Distance = Distance;
             res.Status = Status;
             res.EntryMethod = EntryMethod;
             res.Summary = Summary;
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
