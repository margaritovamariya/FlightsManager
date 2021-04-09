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
        /// <summary>
        /// Гет заявка за показване на регистрация по дадени id, upn.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="UPN"></param>
        /// <returns> Изгледа на страницата </returns>
       

        [HttpGet]
        public IActionResult AddReservation(int id, int UPN)
        {
            if(id == 0)
            {
                return Redirect("/Home/Index");
            }
            ViewBag.id = id;
            ViewBag.UPN = UPN;
            return View();
        }

        /// <summary>
        /// Пост заявка за добавяне на резервация.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="secondName"></param>
        /// <param name="familyName"></param>
        /// <param name="pin"></param>
        /// <param name="email"></param>
        /// <param name="telephoneNumber"></param>
        /// <param name="nationality"></param>
        /// <param name="ticketType"></param>
        /// <param name="uniquePlaneNumber"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddReservation(string firstName, string secondName, string familyName, long pin, string email,
            string telephoneNumber, string nationality, string ticketType, int uniquePlaneNumber)
        {
            if (ModelState.IsValid)
            {
                reservationService.Create(firstName, secondName, familyName, pin, email,
 telephoneNumber, nationality, ticketType, uniquePlaneNumber);
            }

            return Redirect("/Flights/ShowAllFlights");
        }

        //[HttpGet]
        //public IActionResult ShowAddedReservation(string firstName, string secondName, string familyName, long pin, string email,
        //    string telephoneNumber, string nationality, string ticketType, int uniquePlaneNumber)
        //{
        //    this.reservationService.Create(firstName, secondName, familyName, pin, email, telephoneNumber, nationality, ticketType, uniquePlaneNumber);
        //    var reservations = this.reservationService.GetFlightReservations(uniquePlaneNumber);
        //    ViewBag.Reservations = reservations;
        //    return this.View();
        //}
    }
}
