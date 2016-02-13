using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using DotNetNuke.Collections;
using DotNetNuke.Common;
using DotNetNuke.Data;
using DotNetNuke.Framework;
using Albatros.Balises.Core.Data;
using Albatros.Balises.Core.Models.Flights;

namespace Albatros.Balises.Core.Repositories
{

    public class FlightRepository : ServiceLocator<IFlightRepository, FlightRepository>, IFlightRepository
    {
        protected override Func<IFlightRepository> GetFactory()
        {
            return () => new FlightRepository();
        }
        public IEnumerable<Flight> GetFlightsByPilot(int portalId, int userId)
        {
            using (var context = DataContext.Instance())
            {
                return context.ExecuteQuery<Flight>(System.Data.CommandType.Text, "SELECT * FROM {databaseOwner}{objectQualifier}vw_Albatros_Balises_Flights WHERE PortalId=@0 AND UserId=@1", portalId, userId);
            }
        }
        public IPagedList<Flight> GetFlightsByPilot(int portalId, int userId, string searchField, string searchText, string orderByField, string sortOrder, int pageIndex, int pageSize)
        {
            using (var context = DataContext.Instance())
            {
                var sql = String.Format("WHERE PortalId={0} AND UserId={1} AND {2} LIKE '%{3}%' ORDER BY {4} {5}", portalId, userId, searchField, searchText, orderByField, sortOrder.ToUpper());
                var rep = context.GetRepository<Flight>();
                return rep.Find(pageIndex, pageSize, sql);
            }
        }
        public IEnumerable<Flight> GetFlights(int portalId)
        {
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<Flight>();
                return rep.Get(portalId);
            }
        }
        public Flight FindFlight(int portalId, int userId, DateTime takeoffTime)
        {
            using (var context = DataContext.Instance())
            {
                return context.ExecuteSingleOrDefault<Flight>(System.Data.CommandType.Text, "SELECT * FROM {databaseOwner}{objectQualifier}vw_Albatros_Balises_Flights WHERE PortalId=@0 AND UserId=@1 AND TakeoffTime=@2", portalId, userId, takeoffTime);
            }
        }
        public Flight GetFlight(int portalId, int flightId)
        {
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<Flight>();
                return rep.GetById(flightId, portalId);
            }
        }
        public int AddFlight(ref FlightBase flight, int userId)
        {
            Requires.NotNull(flight);
            Requires.PropertyNotNegative(flight, "PortalId");
            flight.CreatedByUserID = userId;
            flight.CreatedOnDate = DateTime.Now;
            flight.LastModifiedByUserID = userId;
            flight.LastModifiedOnDate = DateTime.Now;
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<FlightBase>();
                rep.Insert(flight);
            }
            return flight.FlightId;
        }
        public void DeleteFlight(FlightBase flight)
        {
            Requires.NotNull(flight);
            Requires.PropertyNotNegative(flight, "FlightId");
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<FlightBase>();
                rep.Delete(flight);
            }
        }
        public void DeleteFlight(int portalId, int flightId)
        {
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<FlightBase>();
                rep.Delete("WHERE PortalId = @0 AND FlightId = @1", portalId, flightId);
            }
        }
        public void UpdateFlight(FlightBase flight, int userId)
        {
            Requires.NotNull(flight);
            Requires.PropertyNotNegative(flight, "FlightId");
            flight.LastModifiedByUserID = userId;
            flight.LastModifiedOnDate = DateTime.Now;
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<FlightBase>();
                rep.Update(flight);
            }
        }
    }

    public interface IFlightRepository
    {
        IEnumerable<Flight> GetFlightsByPilot(int portalId, int userId);
        IPagedList<Flight> GetFlightsByPilot(int portalId, int userId, string searchField, string searchText, string orderByField, string sortOrder, int pageIndex, int pageSize);
        IEnumerable<Flight> GetFlights(int portalId);
        Flight FindFlight(int portalId, int userId, DateTime takeoffTime);
        Flight GetFlight(int portalId, int flightId);
        int AddFlight(ref FlightBase flight, int userId);
        void DeleteFlight(FlightBase flight);
        void DeleteFlight(int portalId, int flightId);
        void UpdateFlight(FlightBase flight, int userId);
    }
}

