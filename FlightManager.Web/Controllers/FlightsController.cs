using System;
using FlightManager.Services;
using FlightManager.Services.Models.OutputModels;
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
        public IActionResult AddFlight(FlightViewModel model)
        {
            flightService.Create(model);

            return View();
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



