
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
        public int UserId { get; set; }
        [DataMember]
        public int Category { get; set; }
        [DataMember]
        public string TakeoffDescription { get; set; }
        [DataMember]
        public DateTime TakeoffTime { get; set; }
        [DataMember]
        public string TakeoffCoords { get; set; }
        [DataMember]
        public double TakeoffLatitude { get; set; }
        [DataMember]
        public double TakeoffLongitude { get; set; }
        [DataMember]
        public int? TakeoffAltitude { get; set; }
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
        public int? LandingAltitude { get; set; }
        [DataMember]
        public int? LandingBeaconId { get; set; }
        [DataMember]
        public int? MaxHeight { get; set; }
        [DataMember]
        public double? MaxVario { get; set; }
        [DataMember]
        public double? MaxSpeed { get; set; }
        [DataMember]
        public double? AverageSpeed { get; set; }
        [DataMember]
        public int DurationMins { get; set; }
        [DataMember]
        public int Distance { get; set; }
        [DataMember]
        public int Status { get; set; }
        [DataMember]
        public int TotalPoints { get; set; }
        [DataMember]
        public int EntryMethod { get; set; }
        [DataMember]
        public string Summary { get; set; }
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

            if (flight.UserId > -1)
                UserId = flight.UserId;

            if (flight.Category > -1)
                Category = flight.Category;

            if (!String.IsNullOrEmpty(flight.TakeoffDescription))
                TakeoffDescription = flight.TakeoffDescription;

            TakeoffTime = flight.TakeoffTime;

            if (!String.IsNullOrEmpty(flight.TakeoffCoords))
                TakeoffCoords = flight.TakeoffCoords;

            TakeoffLatitude = flight.TakeoffLatitude;

            TakeoffLongitude = flight.TakeoffLongitude;

            if (flight.TakeoffAltitude > -1)
                TakeoffAltitude = flight.TakeoffAltitude;

            if (!String.IsNullOrEmpty(flight.LandingDescription))
                LandingDescription = flight.LandingDescription;

            LandingTime = flight.LandingTime;

            if (!String.IsNullOrEmpty(flight.LandingCoords))
                LandingCoords = flight.LandingCoords;

            LandingLatitude = flight.LandingLatitude;

            LandingLongitude = flight.LandingLongitude;

            if (flight.LandingAltitude > -1)
                LandingAltitude = flight.LandingAltitude;

            if (flight.LandingBeaconId > -1)
                LandingBeaconId = flight.LandingBeaconId;

            if (flight.MaxHeight > -1)
                MaxHeight = flight.MaxHeight;

            if (flight.MaxVario != null)
            MaxVario = flight.MaxVario;

            if (flight.MaxSpeed != null)
            MaxSpeed = flight.MaxSpeed;

            if (flight.AverageSpeed != null)
            AverageSpeed = flight.AverageSpeed;

            if (flight.DurationMins > -1)
                DurationMins = flight.DurationMins;

            if (flight.Distance > -1)
                Distance = flight.Distance;

            if (flight.Status > -1)
                Status = flight.Status;

            if (flight.TotalPoints > -1)
                TotalPoints = flight.TotalPoints;

            if (flight.EntryMethod > -1)
                EntryMethod = flight.EntryMethod;

            if (!String.IsNullOrEmpty(flight.Summary))
                Summary = flight.Summary;

            if (flight.ValidatedByUserID > -1)
                ValidatedByUserID = flight.ValidatedByUserID;

            if (flight.ValidatedOnDate != null)
            ValidatedOnDate = flight.ValidatedOnDate;

        }
        #endregion

    }
}



