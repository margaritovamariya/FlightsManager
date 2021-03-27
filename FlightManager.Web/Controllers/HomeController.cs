using FlightManager.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FlightManager.Services;

namespace FlightManager.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFlightService flightService;
        private readonly IUserServices userServices;

        public HomeController(IFlightService flightService, IUserServices userServices)
        {
            this.flightService = flightService;
            this.userServices = userServices;
        }

        public IActionResult Index()
        {
            userServices.SeedUserRoles();
            ViewBag.Flights = this.flightService.GetAllFlights();
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
