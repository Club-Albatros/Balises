
using Albatros.DNN.Modules.Balises.Models.FlightBeacons;
using Albatros.DNN.Modules.Balises.Repositories;

namespace Albatros.DNN.Modules.Balises.Controllers
{

 public partial class FlightBeaconsController
 {


  public static void UpdateFlightBeacon(FlightBeaconBase flightBeacon)
  {

   FlightBeaconBaseRepository repo = new FlightBeaconBaseRepository();
   repo.Update(flightBeacon);

  }

  public static void DeleteFlightBeacon(FlightBeaconBase flightBeacon)
  {

   FlightBeaconBaseRepository repo = new FlightBeaconBaseRepository();
   repo.Delete(flightBeacon);

  }

 }
}
