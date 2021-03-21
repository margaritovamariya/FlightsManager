using System;
using System.Collections.Generic;
using System.Text;
using FlightManager.Common;
using FlightManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlightManager.Data.Configurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Username)
                .HasMaxLength(GlobalConstants.UsernameMaxLength)
                .IsUnicode();

            builder.Property(x => x.Password)
                .HasMaxLength(GlobalConstants.PasswordMaxLength)
                .IsUnicode();

            builder.Property(x => x.FirstName)
                .HasMaxLength(GlobalConstants.UserFirstNameMaxLength)
                .IsUnicode();

            builder.Property(x => x.FamilyName)
                .HasMaxLength(GlobalConstants.UserFamilyNameMaxLength)
                .IsUnicode();

            builder.Property(x => x.Email)
                .HasMaxLength(GlobalConstants.EmailMaxLength)
                .IsUnicode(false);
        }
    }
}
