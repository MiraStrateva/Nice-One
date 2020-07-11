namespace NiceOne.Place.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using static NiceOne.Data.Constants.DataConstants;
    using NiceOne.Place.Data.Entities;

    public class PlaceConfiguration : IEntityTypeConfiguration<Place>
    {
        public void Configure(EntityTypeBuilder<Place> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(MaxNameLength);

            builder.Property(p => p.Description)
                .HasMaxLength(MaxDescriptionLength);

            builder.Property(p => p.CityId)
                .IsRequired();
        }
    }
}
