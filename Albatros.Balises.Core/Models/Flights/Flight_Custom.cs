using System.Runtime.Serialization;
using DotNetNuke.ComponentModel.DataAnnotations;
using Albatros.Balises.Core.Common;

namespace Albatros.Balises.Core.Models.Flights
{

    public partial class Flight : FlightBase
    {

        [IgnoreColumn]
        [DataMember]
        public BeaconPassage Start
        {
            get
            {
                return new BeaconPassage()
                {
                    Latitude = this.StartLatitude,
                    Longitude = this.StartLongitude,
                    Altitude = 0,
                    PassageTime = this.FlightStart,
                    Name = this.StartDescription,
                    Description = this.StartDescription
                };
            }
        }

        [IgnoreColumn]
        [DataMember]
        public BeaconPassage Landing
        {
            get
            {
                return new BeaconPassage()
                {
                    Latitude = this.LandingLatitude,
                    Longitude = this.LandingLongitude,
                    Altitude = 0,
                    PassageTime = this.LandingTime,
                    Name = this.LandingDescription,
                    Description = this.LandingDescription
                };
            }
        }

    }
}
