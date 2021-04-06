﻿using System;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddReservation(int id, int planeNumber)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ShowAddedReservation(string firstName, string secondName, string familyName, long pin,
            string telephoneNumber, string nationality, string ticketType, int uniquePlaneNumber)
        {
            this.reservationService.Create(firstName, secondName, familyName, pin, telephoneNumber, nationality, ticketType, uniquePlaneNumber);
            var reservations = this.reservationService.GetFlightReservations(uniquePlaneNumber);
            ViewBag.Reservations = reservations;
            return this.View();
        }
    }
}
