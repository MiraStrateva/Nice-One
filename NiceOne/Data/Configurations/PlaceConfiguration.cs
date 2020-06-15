﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NiceOne.Data.Constants;
using NiceOne.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NiceOne.Data.Configurations
{
    public class PlaceConfiguration : IEntityTypeConfiguration<Place>
    {
        public void Configure(EntityTypeBuilder<Place> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(DataConstants.MaxNameLength);

            builder.Property(p => p.Description)
                .HasMaxLength(DataConstants.MaxDescriptionLength);

            builder.HasOne(p => p.City)
                .WithMany(c => c.Places)
                .HasForeignKey(p => p.CityId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
