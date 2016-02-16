using DotNetNuke.Data;
using DotNetNuke.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using Albatros.Balises.Core.Models.Sites;

namespace Albatros.Balises.Core.Repositories
{
    public class SitesRepository : ServiceLocator<ISitesRepository, SitesRepository>, ISitesRepository
    {
        protected override Func<ISitesRepository> GetFactory()
        {
            return () => new SitesRepository();
        }

        public IEnumerable<Site> GetBeaconTakeoffs(int portalId)
        {
            using (var context = DataContext.Instance())
            {
                return context.ExecuteQuery<Site>(System.Data.CommandType.Text, "SELECT b.Name, b.Latitude, b.Longitude, b.Altitude FROM {databaseOwner}{objectQualifier}Albatros_Balises_Beacons b WHERE b.Code LIKE '%DEC%' AND b.PortalId=@0", portalId);
            }
        }

        public IEnumerable<Site> GetBeaconLandings(int portalId)
        {
            using (var context = DataContext.Instance())
            {
                return context.ExecuteQuery<Site>(System.Data.CommandType.Text, "SELECT b.Name, b.Latitude, b.Longitude, b.Altitude FROM {databaseOwner}{objectQualifier}Albatros_Balises_Beacons b WHERE b.Code LIKE '%ATT%' AND b.PortalId=@0", portalId);
            }
        }

        public Dictionary<string, Site> SearchTakeoffSiteList(int portalId, string searchString)
        {
            using (var context = DataContext.Instance())
            {
                Dictionary<string, Site> res = context.ExecuteQuery<Site>(System.Data.CommandType.Text, "SELECT f.TakeoffDescription Name, AVG(f.TakeoffLatitude) Latitude, AVG(f.TakeoffLongitude) Longitude, AVG(f.TakeoffAltitude) Altitude FROM {databaseOwner}{objectQualifier}Albatros_Balises_Flights f GROUP BY f.TakeoffDescription, f.PortalId HAVING f.TakeoffDescription COLLATE Latin1_general_CI_AI LIKE '%' + @1 + '%' COLLATE Latin1_general_CI_AI AND f.PortalId=@0", portalId, searchString).ToDictionary(s => s.Name, s => s);
                var beacons = GetBeaconTakeoffs(portalId);
                foreach (var b in beacons)
                {
                    if (!res.ContainsKey(b.Name))
                    {
                        res.Add(b.Name, b);
                    }
                }
                return res;
            }
        }

        public Dictionary<string, Site> GetLandingSiteList(int portalId, string searchString)
        {
            using (var context = DataContext.Instance())
            {
                Dictionary<string, Site> res = context.ExecuteQuery<Site>(System.Data.CommandType.Text, "SELECT f.LandingDescription Name, AVG(f.LandingLatitude) Latitude, AVG(f.LandingLongitude) Longitude, AVG(f.LandingAltitude) Altitude FROM {databaseOwner}{objectQualifier}Albatros_Balises_Flights f GROUP BY f.LandingDescription, f.PortalId HAVING f.LandingDescription COLLATE Latin1_general_CI_AI LIKE '%' + @1 + '%' COLLATE Latin1_general_CI_AI AND f.PortalId=@0", portalId, searchString).ToDictionary(s => s.Name, s => s);
                var beacons = GetBeaconLandings(portalId);
                foreach (var b in beacons)
                {
                    if (!res.ContainsKey(b.Name))
                    {
                        res.Add(b.Name, b);
                    }
                }
                return res;
            }
        }

        public void SetNewSite(double latitude, double longitude, string newName, int maxDistance)
        {
            using (var context = DataContext.Instance())
            {
                context.Execute(System.Data.CommandType.StoredProcedure, "{databaseOwner}{objectQualifier}Albatros_Balises_SetNewSite", latitude, longitude, newName, maxDistance);
            }
        }

        public IEnumerable<Site> GetClosestTakeoffSites(int portalId, double latitude, double longitude, int maxDistance)
        {
            using (var context = DataContext.Instance())
            {
                return context.ExecuteQuery<Site>(System.Data.CommandType.Text,
                "SELECT TOP 1 * FROM (SELECT f.TakeoffDescription Name, f.TakeoffLatitude Latitude, f.TakeoffLongitude Longitude, f.TakeoffAltitude Altitude, {databaseOwner}{objectQualifier}Albatros_Balises_CalculateDistance(@1, @2, f.TakeoffLatitude, f.TakeoffLongitude) Dist FROM {databaseOwner}{objectQualifier}Albatros_Balises_Flights f WHERE f.TakeoffDescription <> '' AND f.PortalId=@0) x WHERE x.Dist < @3 ORDER BY x.Dist DESC", portalId, latitude, longitude, maxDistance);
            }
        }

        public IEnumerable<Site> GetClosestLandingSites(int portalId, double latitude, double longitude, int maxDistance)
        {
            using (var context = DataContext.Instance())
            {
                return context.ExecuteQuery<Site>(System.Data.CommandType.Text,
                "SELECT TOP 1 * FROM (SELECT f.LandingDescription Name, f.LandingLatitude Latitude, f.LandingLongitude Longitude, f.TakeoffAltitude Altitude, {databaseOwner}{objectQualifier}Albatros_Balises_CalculateDistance(@1, @2, f.LandingLatitude, f.LandingLongitude) Dist FROM {databaseOwner}{objectQualifier}Albatros_Balises_Flights f WHERE f.LandingDescription <> '' AND f.PortalId=@0) x WHERE x.Dist < @3 ORDER BY x.Dist DESC", portalId, latitude, longitude, maxDistance);
            }
        }

    }

    public interface ISitesRepository
    {
        IEnumerable<Site> GetBeaconTakeoffs(int portalId);
        IEnumerable<Site> GetBeaconLandings(int portalId);
        Dictionary<string, Site> SearchTakeoffSiteList(int portalId, string searchString);
        Dictionary<string, Site> GetLandingSiteList(int portalId, string searchString);
        void SetNewSite(double latitude, double longitude, string newName, int maxDistance);
        IEnumerable<Site> GetClosestTakeoffSites(int portalId, double latitude, double longitude, int maxDistance);
        IEnumerable<Site> GetClosestLandingSites(int portalId, double latitude, double longitude, int maxDistance);
    }
}
