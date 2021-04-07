using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightManager.Services;
using FlightManager.Services.Models.OutputModels;
using Microsoft.AspNetCore.Mvc;

namespace FlightManager.Web.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly IReservationService reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            this.reservationService = reservationService;
        }


        [HttpGet]
        public IActionResult AddReservation(int id, int planeNumber)
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddReservation(string firstName, string secondName, string familyName, long pin, string email,
            string telephoneNumber, string nationality, string ticketType, int uniquePlaneNumber)
        {
            reservationService.Create(firstName, secondName, familyName, pin,email,
             telephoneNumber, nationality, ticketType, uniquePlaneNumber);

            return View();
        }

        [HttpGet]
        public IActionResult ShowAddedReservation(string firstName, string secondName, string familyName, long pin, string email,
            string telephoneNumber, string nationality, string ticketType, int uniquePlaneNumber)
        {
            this.reservationService.Create(firstName, secondName, familyName, pin, email, telephoneNumber, nationality, ticketType, uniquePlaneNumber);
            var reservations = this.reservationService.GetFlightReservations(uniquePlaneNumber);
            ViewBag.Reservations = reservations;
            return this.View();
        }
    }
}
