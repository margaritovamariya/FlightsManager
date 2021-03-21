using System;
using FlightManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightManager.Web.Controllers
{
    public class FlightsController : Controller
    {
        private readonly IFlightService flightService;

        public FlightsController(IFlightService flightService)
        {
            this.flightService = flightService;
        }

        public IActionResult AddFlight()
        {
            return View();
        }

        public IActionResult ShowAddedFlight(string from, string to, DateTime dateTimeTakeOff, DateTime dateTimeLanding, string planeType,
                                                int uniquePlaneNumber, string pilotName, int passengersCapacity, int businessClassCapacity)
        {
            this.flightService.Create(from, to, dateTimeTakeOff, dateTimeLanding, planeType,
                 uniquePlaneNumber, pilotName, passengersCapacity, businessClassCapacity);

            var flight = this.flightService.GetExactFlight(from, to, dateTimeTakeOff, dateTimeLanding, planeType, uniquePlaneNumber);
            return this.View(flight);
        }
    }
}
