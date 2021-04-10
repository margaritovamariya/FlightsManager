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
