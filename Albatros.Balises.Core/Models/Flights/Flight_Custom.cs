using System.Runtime.Serialization;
using DotNetNuke.ComponentModel.DataAnnotations;
using Albatros.Balises.Core.Common;

namespace Albatros.Balises.Core.Models.Flights
{

    public partial class Flight : FlightBase
    {

        [IgnoreColumn]
        [DataMember]
        public BeaconPassage Takeoff
        {
            get
            {
                return new BeaconPassage()
                {
                    Latitude = this.TakeoffLatitude,
                    Longitude = this.TakeoffLongitude,
                    Altitude = 0,
                    PassageTime = this.TakeoffTime,
                    Name = this.TakeoffDescription,
                    Description = this.TakeoffDescription
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
