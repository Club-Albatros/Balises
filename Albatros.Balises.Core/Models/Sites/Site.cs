using DotNetNuke.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Albatros.Balises.Core.Common;

namespace Albatros.Balises.Core.Models.Sites
{
    [DataContract]
    public class Site
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public double Latitude { get; set; }
        [DataMember]
        public double Longitude { get; set; }
        [DataMember]
        public int Altitude { get; set; }
        [DataMember]
        [IgnoreColumn]
        public string Coords
        {
            get
            {
                return SwissProjection.ToSwissCoordinates(Latitude, Longitude);
            }
        }
    }
}
