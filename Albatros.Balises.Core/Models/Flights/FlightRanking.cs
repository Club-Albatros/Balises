namespace Albatros.Balises.Core.Models.Flights
{
    public class FlightRanking : Flight
    {
        public int FlightRank { get; set; }
        public int AggregatePoints { get; set; }
        public int PilotRanking { get; set; }
    }
}
