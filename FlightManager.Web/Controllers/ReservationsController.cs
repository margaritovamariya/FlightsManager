using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightManager.Services;
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

        public IActionResult AddReservation()
        {
            return View();
        }

        public IActionResult ShowAddedReservation(string firstName, string secondName, string familyName, long pin,
            string telephoneNumber, string nationality, string ticketType, int uniquePlaneNumber)
        {
            this.reservationService.Create(firstName, secondName, familyName, pin, telephoneNumber, nationality, ticketType, uniquePlaneNumber);
            var reservations = this.reservationService.GetFlightReservations(uniquePlaneNumber);
            return this.View(reservations);
        }
    }
}
