using FlightManager.Services;
using FlightManager.Services.Models.OutputModels;
using Microsoft.AspNetCore.Mvc;

namespace FlightManager.Web.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly IReservationService reservationService;
        private readonly IFlightService flightService;

        public ReservationsController(IReservationService reservationService, IFlightService flightService)
        {
            this.reservationService = reservationService;
            this.flightService = flightService;
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
        public IActionResult AddReservation(ReservationListViewModel reservationListView, int uniquePlaneNumber, string Email)
        {
            foreach(var emails in reservationListView.reservations)
            {
                emails.Email = Email;
            }

            if (ModelState.IsValid)
            {
                reservationService.Create(reservationListView, uniquePlaneNumber);
            }

            return Redirect("/Flights/ShowAllFlights");
        }

        [HttpGet]
        public IActionResult ShowAllReservations(ReservationTableViewModel model, int uniquePlaneNumber)
        {
            var reservations = this.reservationService.GetFlightReservations(uniquePlaneNumber);
            ViewBag.Reservations = reservations;
            var flight = this.flightService.GetExactFlight(uniquePlaneNumber);
            var reservationPages = this.reservationService.ReturnPages(model);
            ViewBag.ReturnedReservations = reservationPages.Result.Items;
            ViewBag.ReturnedReservationPagers = reservationPages.Result.Pager.PagesCount;
            ViewBag.ReturnedReservationPagersCurrentPage = reservationPages.Result.Pager.CurrentPage;
            return View(flight);
        }


    }
}
