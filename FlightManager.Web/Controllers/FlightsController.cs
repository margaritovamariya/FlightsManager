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

        /// <summary>
        /// Гет заявка за полет
        /// </summary>
        /// <returns>Изгледа на страницата </returns>
        [HttpGet]
        public IActionResult AddFlight()
        {
            return View();
        }

        /// <summary>
        /// Пост заявка за добавяне на полет
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="dateTimeTakeOff"></param>
        /// <param name="dateTimeLanding"></param>
        /// <param name="planeType"></param>
        /// <param name="uniquePlaneNumber"></param>
        /// <param name="pilotName"></param>
        /// <param name="passengersCapacity"></param>
        /// <param name="businessClassCapacity"></param>
        /// <returns>редиректва ни към showAllFlights</returns>
        [HttpPost]
        public IActionResult AddFlight(string from, string to, DateTime dateTimeTakeOff, DateTime dateTimeLanding, string planeType,
                                                int uniquePlaneNumber, string pilotName, int passengersCapacity, int businessClassCapacity)
        {
            flightService.Create(from, to, dateTimeTakeOff, dateTimeLanding, planeType,
                                                 uniquePlaneNumber, pilotName, passengersCapacity, businessClassCapacity);

            return Redirect("ShowAllFlights");
        }
        /// <summary>
        /// Гет заявка за показване на всички полети.
        /// </summary>
        /// <returns>Изгледа на страницата</returns>
        [HttpGet]
        public IActionResult ShowAllFlights()
        {
            var flights = this.flightService.GetAllFlights();
            ViewBag.Flights = flights;
            return this.View();
        }
    }
}



