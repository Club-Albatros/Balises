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
                return context.ExecuteQuery<Site>(System.Data.CommandType.Text, "SELECT b.Name, b.Latitude, b.Longitude FROM {databaseOwner}{objectQualifier}Albatros_Balises_Beacons b WHERE b.Code LIKE '%DEC%' AND b.PortalId=@0", portalId);
            }
        }

        public IEnumerable<Site> GetBeaconLandings(int portalId)
        {
            using (var context = DataContext.Instance())
            {
                return context.ExecuteQuery<Site>(System.Data.CommandType.Text, "SELECT b.Name, b.Latitude, b.Longitude FROM {databaseOwner}{objectQualifier}Albatros_Balises_Beacons b WHERE b.Code LIKE '%ATT%' AND b.PortalId=@0", portalId);
            }
        }

        public Dictionary<string, Site> GetTakeoffSiteList(int portalId)
        {
            using (var context = DataContext.Instance())
            {
                Dictionary<string, Site> res = context.ExecuteQuery<Site>(System.Data.CommandType.Text, "SELECT f.TakeoffDescription Name, AVG(f.TakeoffLatitude) Latitude, AVG(f.TakeoffLongitude) Longitude FROM {databaseOwner}{objectQualifier}Albatros_Balises_Flights f GROUP BY f.TakeoffDescription, f.PortalId HAVING f.TakeoffDescription <> '' AND f.PortalId=@0", portalId).ToDictionary(s => s.Name, s => s);
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

        public Dictionary<string, Site> GetLandingSiteList(int portalId)
        {
            using (var context = DataContext.Instance())
            {
                Dictionary<string, Site> res = context.ExecuteQuery<Site>(System.Data.CommandType.Text, "SELECT f.LandingDescription Name, AVG(f.LandingLatitude) Latitude, AVG(f.LandingLongitude) Longitude FROM {databaseOwner}{objectQualifier}Albatros_Balises_Flights f GROUP BY f.LandingDescription, f.PortalId HAVING f.LandingDescription <> '' AND f.PortalId=@0", portalId).ToDictionary(s => s.Name, s => s);
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

    }

    public interface ISitesRepository
    {
        IEnumerable<Site> GetBeaconTakeoffs(int portalId);
        IEnumerable<Site> GetBeaconLandings(int portalId);
        Dictionary<string, Site> GetTakeoffSiteList(int portalId);
        Dictionary<string, Site> GetLandingSiteList(int portalId);
        void SetNewSite(double latitude, double longitude, string newName, int maxDistance);
    }
}
