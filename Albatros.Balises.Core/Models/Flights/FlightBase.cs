
using System;
using System.Runtime.Serialization;
using DotNetNuke.ComponentModel.DataAnnotations;
using Albatros.Balises.Core.Common;
using Albatros.Balises.Core.Data;

namespace Albatros.Balises.Core.Models.Flights
{
    [TableName("Albatros_Balises_Flights")]
    [PrimaryKey("FlightId", AutoIncrement = true)]
    [DataContract]
    [Scope("PortalId")]
    public partial class FlightBase  : AuditableEntity 
    {

        #region .ctor
        public FlightBase()
        {
            FlightId = -1;
        }
        #endregion

        #region Properties
        [DataMember]
        public int FlightId { get; set; }
        [DataMember]
        public int PortalId { get; set; }
        [DataMember]
        public DateTime FlightStart { get; set; }
        [DataMember]
        public int DurationMins { get; set; }
        [DataMember]
        public int Distance { get; set; }
        [DataMember]
        public int Category { get; set; }
        [DataMember]
        public string StartDescription { get; set; }
        [DataMember]
        public string StartCoords { get; set; }
        [DataMember]
        public double StartLatitude { get; set; }
        [DataMember]
        public double StartLongitude { get; set; }
        [DataMember]
        public string LandingDescription { get; set; }
        [DataMember]
        public DateTime LandingTime { get; set; }
        [DataMember]
        public string LandingCoords { get; set; }
        [DataMember]
        public double LandingLatitude { get; set; }
        [DataMember]
        public double LandingLongitude { get; set; }
        [DataMember]
        public string Summary { get; set; }
        [DataMember]
        public bool Validated { get; set; }
        [DataMember]
        public bool Rejected { get; set; }
        [DataMember]
        public int? ValidatedByUserID { get; set; }
        [DataMember]
        public DateTime? ValidatedOnDate { get; set; }
        #endregion

        #region Methods
        public void ReadFlightBase(FlightBase flight)
        {
            if (flight.FlightId > -1)
                FlightId = flight.FlightId;

            if (flight.PortalId > -1)
                PortalId = flight.PortalId;

            FlightStart = flight.FlightStart;

            if (flight.DurationMins > -1)
                DurationMins = flight.DurationMins;

            if (flight.Distance > -1)
                Distance = flight.Distance;

            if (flight.Category > -1)
                Category = flight.Category;

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

            Rejected = flight.Rejected;

            if (flight.ValidatedByUserID > -1)
                ValidatedByUserID = flight.ValidatedByUserID;

            if (flight.ValidatedOnDate != null)
            ValidatedOnDate = flight.ValidatedOnDate;

        }
        #endregion

    }
}


