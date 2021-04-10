using FlightManager.Services;
using FlightManager.Services.Models.OutputModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Threading.Tasks;

namespace FlightManager.Web.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly IReservationService reservationService;
        private readonly IFlightService flightService;
        private readonly IMailService mailService;

        public ReservationsController(IReservationService reservationService, IFlightService flightService, IMailService mail)
        {
            this.reservationService = reservationService;
            this.flightService = flightService;
            this.mailService = mail;
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
            if (id == 0)
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
        /// <param name="reservationListView"></param>
        /// <param name="Email"></param>
        /// <param name="uniquePlaneNumber"></param>
        /// <returns> Redirect to AllFlights </returns>
        [HttpPost]
        public async Task<IActionResult> AddReservation(ReservationListViewModel reservationListView, int uniquePlaneNumber, string Email)
        {
            var flight = flightService.GetExactFlight(uniquePlaneNumber);
            string HtmlFlightData = "";
            string ResrvData = "";
            var x = 0;
            string[] htmlResrvData = new string[reservationListView.Reservations.Count];

            foreach (var resrvs in reservationListView.Reservations)
            {
                
                ResrvData = $"<p><span>First Name: {resrvs.FirstName} || Second Name: {resrvs.SecondName} || FamilyName: {resrvs.SecondName} || PIN: {resrvs.PIN} ||Phonenumber: {resrvs.TelephoneNumber} || Nationality: {resrvs.Nationality} || TicketType: {resrvs.TicketType} </span></p>" +
                    $"<hr/>";
                htmlResrvData[x] = ResrvData;
                x++;
            }

            foreach (var flights in flight)
            {
                HtmlFlightData = $"<h1>Hi, Your reservation has been successful</h1>" +
                    $"<p><b> Flight Details </b></p>" +
               $"<p>From: {flights.From}</p>" +
               $"<p>To: {flights.To}</p>" +
               $"<p>Plane Type: {flights.PlaneType}</p>" +
               $"<p>Departur on: {flights.DateTimeTakeOff}</p>" +
               $"<h4> Reservation Details </h4>"
               ;
            }

            var sb = new StringBuilder();

            for (int i = 0; i < htmlResrvData.Length; i++)
                sb.Append(htmlResrvData[i]);

            foreach (var emails in reservationListView.Reservations)
            {
                emails.Email = Email;
            }

            if (ModelState.IsValid)
            {
                reservationService.Create(reservationListView, uniquePlaneNumber);
                await mailService.SendEmailAsync(Email, "FlightManager", HtmlFlightData + sb);
            }

            return Redirect("/Home/Index");
        }

        /// <summary>
        /// Shows all reservation for given flight
        /// </summary>
        /// <param name="model"></param>
        /// <param name="uniquePlaneNumber"></param>
        /// <returns>List of reservations </returns>
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
