using System.Collections.Generic;

namespace FlightManager.Services.Models.OutputModels
{
    public class ReservationTableViewModel
    {
        public PagerViewModel Pager { get; set; }
        public ICollection<ReservationViewModel> Items { get; set; }
    }
}
