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

        [HttpGet]
        public IActionResult AddFlight()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddFlight(string from, string to, DateTime dateTimeTakeOff, DateTime dateTimeLanding, string planeType,
                                                int uniquePlaneNumber, string pilotName, int passengersCapacity, int businessClassCapacity)
        {
            flightService.Create(from, to, dateTimeTakeOff, dateTimeLanding, planeType,
                                                 uniquePlaneNumber, pilotName, passengersCapacity, businessClassCapacity);

            return Redirect("ShowAllFlights");
        }

        [HttpGet]
        public IActionResult ShowAllFlights()
        {
            var flights = this.flightService.GetAllFlights();
            ViewBag.Flights = flights;
            return this.View();
        }
    }
}



