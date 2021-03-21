using System;
using System.Collections.Generic;
using System.Text;
using FlightManager.Common;
using FlightManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlightManager.Data.Configurations
{
    public class ReservationEntityConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.Property(x => x.FirstName)
                .HasMaxLength(GlobalConstants.FirstNameMaxLength)
                .IsUnicode();

            builder.Property(x => x.SecondName)
                .HasMaxLength(GlobalConstants.SecondNameMaxLength)
                .IsUnicode();

            builder.Property(x => x.FamilyName)
                .HasMaxLength(GlobalConstants.FamilyNameMaxLength)
                .IsUnicode();
        }
    }
}
