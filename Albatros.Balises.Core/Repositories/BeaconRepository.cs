using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using DotNetNuke.Collections;
using DotNetNuke.Common;
using DotNetNuke.Data;
using DotNetNuke.Framework;
using Albatros.Balises.Core.Data;
using Albatros.Balises.Core.Models.Beacons;

namespace Albatros.Balises.Core.Repositories
{

    public class BeaconRepository : ServiceLocator<IBeaconRepository, BeaconRepository>, IBeaconRepository
    {
        protected override Func<IBeaconRepository> GetFactory()
        {
            return () => new BeaconRepository();
        }
        public IEnumerable<Beacon> GetBeacons(int portalId)
        {
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<Beacon>();
                return rep.Get(portalId);
            }
        }
        public Beacon GetBeacon(int portalId, int beaconId)
        {
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<Beacon>();
                return rep.GetById(beaconId, portalId);
            }
        }
        public int AddBeacon(ref BeaconBase beacon, int userId)
        {
            Requires.NotNull(beacon);
            Requires.PropertyNotNegative(beacon, "PortalId");
            beacon.CreatedByUserID = userId;
            beacon.CreatedOnDate = DateTime.Now;
            beacon.LastModifiedByUserID = userId;
            beacon.LastModifiedOnDate = DateTime.Now;
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<BeaconBase>();
                rep.Insert(beacon);
            }
            return beacon.BeaconId;
        }
        public void DeleteBeacon(BeaconBase beacon)
        {
            Requires.NotNull(beacon);
            Requires.PropertyNotNegative(beacon, "BeaconId");
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<BeaconBase>();
                rep.Delete(beacon);
            }
        }
        public void DeleteBeacon(int portalId, int beaconId)
        {
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<BeaconBase>();
                rep.Delete("WHERE PortalId = @0 AND BeaconId = @1", portalId, beaconId);
            }
        }
        public void UpdateBeacon(BeaconBase beacon, int userId)
        {
            Requires.NotNull(beacon);
            Requires.PropertyNotNegative(beacon, "BeaconId");
            beacon.LastModifiedByUserID = userId;
            beacon.LastModifiedOnDate = DateTime.Now;
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<BeaconBase>();
                rep.Update(beacon);
            }
        }
        public IEnumerable<Beacon> GetClosestBeacon(int portalId, double latitude, double longitude, int maxDistance)
        {
            using (var context = DataContext.Instance())
            {
                return context.ExecuteQuery<Beacon>(System.Data.CommandType.Text,
                "SELECT TOP 1 * FROM (SELECT b.*, {databaseOwner}{objectQualifier}Albatros_Balises_CalculateDistance(@1, @2, b.Latitude, b.Longitude) Dist FROM {databaseOwner}{objectQualifier}vw_Albatros_Balises_Beacons b WHERE b.PortalId=@0) x WHERE x.Dist < @3 ORDER BY x.Dist DESC", portalId, latitude, longitude, maxDistance);
            }
        }
    }

    public interface IBeaconRepository
    {
        IEnumerable<Beacon> GetBeacons(int portalId);
        Beacon GetBeacon(int portalId, int beaconId);
        int AddBeacon(ref BeaconBase beacon, int userId);
        void DeleteBeacon(BeaconBase beacon);
        void DeleteBeacon(int portalId, int beaconId);
        void UpdateBeacon(BeaconBase beacon, int userId);
        IEnumerable<Beacon> GetClosestBeacon(int portalId, double latitude, double longitude, int maxDistance);
    }
}

