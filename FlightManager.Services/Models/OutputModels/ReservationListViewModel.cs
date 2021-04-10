using System;
using System.Collections.Generic;
using System.Text;

namespace FlightManager.Services.Models.OutputModels
{
    /// <summary>
    /// ReservationListModel
    /// </summary>
    public class ReservationListViewModel
    {
        /// <summary>
        /// gets,sets List of Reservations
        /// </summary>
        public List<ReservationViewModel> Reservations { get; set; }
    }
}
