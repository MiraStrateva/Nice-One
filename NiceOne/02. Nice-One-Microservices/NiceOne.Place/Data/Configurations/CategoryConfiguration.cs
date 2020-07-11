namespace NiceOne.Place.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using static NiceOne.Data.Constants.DataConstants;
    using NiceOne.Place.Data.Entities;

    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(MaxNameLength);

            builder.Property(c => c.Description)
                .HasMaxLength(MaxDescriptionLength);

            builder.Property(c => c.ImageUrl)
                .IsRequired()
                .HasMaxLength(MaxUrlLength);
        }
    }
}
