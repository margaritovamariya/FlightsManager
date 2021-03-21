using FlightManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlightManager.Data.Configurations
{
    public class ReservationFlightEntityConfiguration : IEntityTypeConfiguration<ReservationFlight>
    {
        public void Configure(EntityTypeBuilder<ReservationFlight> builder)
        {
            builder.HasKey(ck => new {ck.ReservationId, ck.FlightId});
        }
    }
}
