
using System.Collections.Generic;
using Albatros.DNN.Modules.Balises.Models.Beacons;
using Albatros.DNN.Modules.Balises.Repositories;

namespace Albatros.DNN.Modules.Balises.Controllers
{

    public partial class BeaconsController
    {

        public static IEnumerable<Beacon> GetBeacons(int portalId)
        {
            BeaconRepository repo = new BeaconRepository();
            return repo.Get(portalId);
        }

        public static Beacon GetBeacon(int beaconId)
        {

            BeaconRepository repo = new BeaconRepository();
            return repo.GetById(beaconId);

        }

        public static int AddBeacon(ref BeaconBase beacon)
        {

            BeaconBaseRepository repo = new BeaconBaseRepository();
            repo.Insert(beacon);
            return beacon.BeaconId;

        }

        public static void UpdateBeacon(BeaconBase beacon)
        {

            BeaconBaseRepository repo = new BeaconBaseRepository();
            repo.Update(beacon);

        }

        public static void DeleteBeacon(BeaconBase beacon)
        {

            BeaconBaseRepository repo = new BeaconBaseRepository();
            repo.Delete(beacon);

        }

    }
}
