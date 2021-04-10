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
        /// <param name="model"></param>
        /// <returns>редиректва ни към showAllFlights</returns>
        [HttpPost]
        public IActionResult AddFlight(FlightViewModel model)
        {
            flightService.Create(model);

            return View();
        }
        /// <summary>
        /// Гет заявка за показване на всички полети.
        /// </summary>
        /// <returns>Всички полети на страницата</returns>
        [HttpGet]
        public IActionResult ShowAllFlights()
        {
            var flights = this.flightService.GetAllFlights();
            ViewBag.Flights = flights;
            return this.View();
        }
    }
}



