using FlightManager.Common;
using FlightManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlightManager.Data.Configurations
{
    public class FlightEntityConfiguration : IEntityTypeConfiguration<Flight>
    {
        public void Configure(EntityTypeBuilder<Flight> builder)
        {
            builder.Property(x => x.From)
                .HasMaxLength(GlobalConstants.FromMaxLength)
                .IsUnicode();

            builder.Property(x => x.To)
                .HasMaxLength(GlobalConstants.ToMaxLength)
                .IsUnicode();
            
        }
    }
}
