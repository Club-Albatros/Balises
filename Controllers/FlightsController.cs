
using Albatros.DNN.Modules.Balises.Models.Flights;
using Albatros.DNN.Modules.Balises.Repositories;

namespace Albatros.DNN.Modules.Balises.Controllers
{

 public partial class FlightsController
 {

  public static Flight GetFlight(int flightId)
  {

    FlightRepository repo = new FlightRepository();
    return repo.GetById(flightId);

  }

  public static int AddFlight(ref FlightBase flight)
  {

   FlightBaseRepository repo = new FlightBaseRepository();
   repo.Insert(flight);
   return flight.FlightId;

  }

  public static void UpdateFlight(FlightBase flight)
  {

   FlightBaseRepository repo = new FlightBaseRepository();
   repo.Update(flight);

  }

  public static void DeleteFlight(FlightBase flight)
  {

   FlightBaseRepository repo = new FlightBaseRepository();
   repo.Delete(flight);

  }

 }
}
