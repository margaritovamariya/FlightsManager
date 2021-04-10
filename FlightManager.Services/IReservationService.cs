using System.Collections.Generic;
using System.Threading.Tasks;
using FlightManager.Models;
using FlightManager.Services.Models.OutputModels;

namespace FlightManager.Services
{
    /// <summary>
    /// ReservationServices Interface
    /// </summary>
    public interface IReservationService
    {
        /// <summary>
        /// Calls Create method from ReservationServices
        /// </summary>
        /// <param name="reservationView"></param>
        /// <param name="uniquePlaneNumber"></param>
        //Create
        void Create(ReservationListViewModel reservationView, int uniquePlaneNumber);

        /// <summary>
        /// Calls GetFlightReservations method from ReservationServices
        /// </summary>
        /// <param name="uniquePlaneNumber"></param>
        /// <returns> Reservations for the given Flight </returns>
        //Read
        IEnumerable<ReservationViewModel> GetFlightReservations(int uniquePlaneNumber);

        /// <summary>
        /// Calls ReturnPages method from ReservationServices
        /// </summary>
        /// <param name="model"></param>
        /// <returns> List and pages of ReservationTableViewModel </returns>
        //Pages
        public Task<ReservationTableViewModel> ReturnPages(ReservationTableViewModel model);
    }
}
