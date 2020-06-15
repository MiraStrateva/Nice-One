using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NiceOne.Data.Constants;
using NiceOne.Data.Entities;

namespace NiceOne.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(DataConstants.MaxNameLength);

            builder.Property(c => c.Description)
                .HasMaxLength(DataConstants.MaxDescriptionLength);

            builder.Property(c => c.ImageUrl)
                .IsRequired()
                .HasMaxLength(DataConstants.MaxUrlLength);
        }
    }
}
