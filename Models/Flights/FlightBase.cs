
using System;
using System.Runtime.Serialization;
using DotNetNuke.ComponentModel.DataAnnotations;
using Albatros.DNN.Modules.Balises.Common;
using Albatros.DNN.Modules.Balises.Data;

namespace Albatros.DNN.Modules.Balises.Models.Flights
{
    [TableName("Albatros_Balises_Flights")]
    [PrimaryKey("FlightId", AutoIncrement = true)]
    [DataContract]
     [Scope("PortalId")]
    public partial class FlightBase  : AuditableEntity 
    {

        #region " Public Properties "
        [DataMember()]
        public int FlightId { get; set; }
        [DataMember()]
        public int PortalId { get; set; }
        [DataMember()]
        public DateTime FlightStart { get; set; }
        [DataMember()]
        public int Category { get; set; }
        [DataMember()]
        public string Comments { get; set; }
        [DataMember()]
        public string StartDescription { get; set; }
        [DataMember()]
        public string StartCoords { get; set; }
        [DataMember()]
        public double StartLatitude { get; set; }
        [DataMember()]
        public double StartLongitude { get; set; }
        [DataMember()]
        public string LandingDescription { get; set; }
        [DataMember()]
        public DateTime LandingTime { get; set; }
        [DataMember()]
        public string LandingCoords { get; set; }
        [DataMember()]
        public double LandingLatitude { get; set; }
        [DataMember()]
        public double LandingLongitude { get; set; }
        [DataMember()]
        public string Summary { get; set; }
        [DataMember()]
        public bool Validated { get; set; }
        [DataMember()]
        public int? ValidatedByUserID { get; set; }
        [DataMember()]
        public DateTime? ValidatedOnDate { get; set; }
        [DataMember()]
        public bool Rejected { get; set; }
        #endregion

        #region " Methods "
        public void ReadFlightBase(FlightBase flight)
        {
            if (flight.FlightId > -1)
                FlightId = flight.FlightId;

            if (flight.PortalId > -1)
                PortalId = flight.PortalId;

            FlightStart = flight.FlightStart;

            if (flight.Category > -1)
                Category = flight.Category;

            if (!String.IsNullOrEmpty(flight.Comments))
                Comments = flight.Comments;

            if (!String.IsNullOrEmpty(flight.StartDescription))
                StartDescription = flight.StartDescription;

            if (!String.IsNullOrEmpty(flight.StartCoords))
                StartCoords = flight.StartCoords;

            StartLatitude = flight.StartLatitude;

            StartLongitude = flight.StartLongitude;

            if (!String.IsNullOrEmpty(flight.LandingDescription))
                LandingDescription = flight.LandingDescription;

            LandingTime = flight.LandingTime;

            if (!String.IsNullOrEmpty(flight.LandingCoords))
                LandingCoords = flight.LandingCoords;

            LandingLatitude = flight.LandingLatitude;

            LandingLongitude = flight.LandingLongitude;

            if (!String.IsNullOrEmpty(flight.Summary))
                Summary = flight.Summary;

            Validated = flight.Validated;

            if (flight.ValidatedByUserID > -1)
                ValidatedByUserID = flight.ValidatedByUserID;

            if (flight.ValidatedOnDate != null)
            ValidatedOnDate = flight.ValidatedOnDate;

            Rejected = flight.Rejected;

        }
        #endregion

    }
}



