using System.Collections.Generic;

namespace FlightManager.Services.Models.OutputModels
{
    /// <summary>
    /// ReservationTableModel
    /// </summary>
    public class ReservationTableViewModel
    {
        /// <summary>
        /// Gets,sets PagerModel pager
        /// </summary>
        public PagerViewModel Pager { get; set; }
        /// <summary>
        /// gets,sets Icollection of reservations
        /// </summary>
        public ICollection<ReservationViewModel> Items { get; set; }
    }
}
