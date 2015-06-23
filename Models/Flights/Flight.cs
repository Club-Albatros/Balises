
using System.Runtime.Serialization;
using DotNetNuke.ComponentModel.DataAnnotations;

namespace Albatros.DNN.Modules.Balises.Models.Flights
{

    [TableName("vw_Albatros_Balises_Flights")]
    [PrimaryKey("FlightId", AutoIncrement = true)]
    [DataContract]
    [Scope("PortalId")]                
    public partial class Flight  : FlightBase 
    {

        #region " Private Members "
        #endregion

        #region " Constructors "
        public Flight()  : base() 
        {
        }
        #endregion

        #region " Public Properties "
        [DataMember()]
        public string CreatedByUser { get; set; }
        [DataMember()]
        public string LastModifiedByUser { get; set; }
        [DataMember()]
        public string ValidatedByUser { get; set; }
        #endregion

        #region " Public Methods "
        public FlightBase GetFlightBase()
        {
            FlightBase res = new FlightBase();
             res.FlightId = FlightId;
             res.PortalId = PortalId;
             res.FlightStart = FlightStart;
             res.Category = Category;
             res.Comments = Comments;
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
             res.ValidatedByUserID = ValidatedByUserID;
             res.ValidatedOnDate = ValidatedOnDate;
             res.Rejected = Rejected;
            return res;
        }
        #endregion

    }
}
