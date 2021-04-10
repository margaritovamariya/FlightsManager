using System.Collections.Generic;
using System.Threading.Tasks;
using FlightManager.Models;
using FlightManager.Services.Models.OutputModels;

namespace FlightManager.Services
{
    public interface IReservationService
    {
        //Create
        void Create(ReservationListViewModel reservationView, int uniquePlaneNumber);

        //Read
        IEnumerable<ReservationViewModel> GetFlightReservations(int uniquePlaneNumber);

        //Update
        void UpdateReservation(Reservation reservation);

        //Pages
        public Task<ReservationTableViewModel> ReturnPages(ReservationTableViewModel model);
    }
}
