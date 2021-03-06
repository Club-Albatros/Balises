using System;
using System.Collections.Generic;
using DotNetNuke.Common;
using DotNetNuke.Data;
using DotNetNuke.Framework;
using Albatros.Balises.Core.Models.FlightBeacons;
using Albatros.Balises.Core.Common;

namespace Albatros.Balises.Core.Repositories
{

    public class FlightBeaconRepository : ServiceLocator<IFlightBeaconRepository, FlightBeaconRepository>, IFlightBeaconRepository
    {
        protected override Func<IFlightBeaconRepository> GetFactory()
        {
            return () => new FlightBeaconRepository();
        }
        public IEnumerable<FlightBeacon> GetFlightBeaconsByFlight(int flightId)
        {
            using (var context = DataContext.Instance())
            {
                return context.ExecuteQuery<FlightBeacon>(System.Data.CommandType.Text,
                    "SELECT * FROM {databaseOwner}{objectQualifier}vw_Albatros_Balises_FlightBeacons WHERE FlightId=@0 ORDER BY PassOrder",
                    flightId);
            }
        }
        public FlightBeacon GetFlightBeacon(int flightId, int beaconId)
        {
            using (var context = DataContext.Instance())
            {
                return context.ExecuteSingleOrDefault<FlightBeacon>(System.Data.CommandType.Text,
                    "SELECT * FROM {databaseOwner}{objectQualifier}vw_Albatros_Balises_FlightBeacons WHERE FlightId=@0 AND BeaconId=@1",
                    flightId, beaconId);
            }
        }
        public void AddFlightBeacon(FlightBeaconBase flightBeacon)
        {
            Requires.NotNull(flightBeacon);
            Requires.NotNull(flightBeacon.FlightId);
            using (var context = DataContext.Instance())
            {
                context.Execute(System.Data.CommandType.Text,
                    "IF NOT EXISTS (SELECT * FROM {databaseOwner}{objectQualifier}Albatros_Balises_FlightBeacons " +
                    "WHERE FlightId=@0 AND BeaconId=@1) " +
                    "INSERT INTO {databaseOwner}{objectQualifier}Albatros_Balises_FlightBeacons (FlightId, BeaconId, PassOrder, PassedDistance) " +
                    "SELECT @0, @1, @2, @3", flightBeacon.FlightId, flightBeacon.BeaconId, flightBeacon.PassOrder, flightBeacon.PassedDistance);
            }
        }
        public void DeleteFlightBeacon(FlightBeaconBase flightBeacon)
        {
            DeleteFlightBeacon(flightBeacon.FlightId, flightBeacon.BeaconId);
        }
        public void DeleteFlightBeacon(int flightId, int beaconId)
        {
            Requires.NotNull(flightId);
            using (var context = DataContext.Instance())
            {
                context.Execute(System.Data.CommandType.Text,
                    "DELETE FROM {databaseOwner}{objectQualifier}Albatros_Balises_FlightBeacons WHERE FlightId=@0 AND BeaconId=@1",
                    flightId, beaconId);
            }
        }
        public void DeleteFlightBeaconsByFlight(int flightId)
        {
            using (var context = DataContext.Instance())
            {
                context.Execute(System.Data.CommandType.Text,
                    "DELETE FROM {databaseOwner}{objectQualifier}Albatros_Balises_FlightBeacons WHERE FlightId=@0",
                    flightId);
            }
        }
        public void UpdateFlightBeacon(FlightBeaconBase flightBeacon)
        {
            Requires.NotNull(flightBeacon);
            Requires.NotNull(flightBeacon.FlightId);
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<FlightBeaconBase>();
                rep.Update("SET PassOrder=@0, PassedDistance=@1 WHERE FlightId=@2 AND BeaconId=@3",
                          flightBeacon.PassOrder, flightBeacon.PassedDistance, flightBeacon.FlightId, flightBeacon.BeaconId);
            }
        }

        public void ProcessFlightBeacons(int flightId, DateTime flightStart, IEnumerable<PathBeacon> path)
        {
            DeleteFlightBeaconsByFlight(flightId);
            var passOrder = 1;
            foreach (var b in path)
            {
                var newBeacon = new FlightBeaconBase()
                {
                    FlightId = flightId,
                    BeaconId = b.BeaconId,
                    PassedDistance = 0,
                    PassOrder = b.PassOrder
                };
                AddFlightBeacon(newBeacon);
                passOrder += 1;
            }
        }
    }

    public interface IFlightBeaconRepository
    {
        IEnumerable<FlightBeacon> GetFlightBeaconsByFlight(int flightId);
        FlightBeacon GetFlightBeacon(int flightId, int beaconId);
        void AddFlightBeacon(FlightBeaconBase flightBeacon);
        void DeleteFlightBeacon(FlightBeaconBase flightBeacon);
        void DeleteFlightBeacon(int flightId, int beaconId);
        void DeleteFlightBeaconsByFlight(int flightId);
        void UpdateFlightBeacon(FlightBeaconBase flightBeacon);
        void ProcessFlightBeacons(int flightId, DateTime flightStart, IEnumerable<PathBeacon> path);
    }
}

