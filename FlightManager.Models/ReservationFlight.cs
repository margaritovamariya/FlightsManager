namespace FlightManager.Models
{
    public class ReservationFlight
    {
        public int ReservationId { get; set; }
        public virtual Reservation Reservation { get; set; }

        public int FlightId { get; set; }
        public virtual Flight Flight { get; set; }
    }
}
