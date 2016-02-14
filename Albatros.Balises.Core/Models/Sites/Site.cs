using System.Runtime.Serialization;

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
    }
}
