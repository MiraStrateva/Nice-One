using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using NiceOne.Data.Constants;
using NiceOne.Data.Entities;
using System;

namespace NiceOne.Data.Configurations
{
    public class PictureConfiduration : IEntityTypeConfiguration<Picture>
    {
        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder.HasKey(p => p.Id);
            
            builder.Property(p => p.PictureUrl)
                .IsRequired()
                .HasMaxLength(DataConstants.MaxUrlLength);

            builder
                .HasOne(p => p.Place)
                .WithMany(p => p.Pictures)
                .HasForeignKey(p => p.PlaceId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
