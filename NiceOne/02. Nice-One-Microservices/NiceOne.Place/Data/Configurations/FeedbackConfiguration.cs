namespace NiceOne.Place.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    
    using static NiceOne.Data.Constants.DataConstants;
    using NiceOne.Place.Data.Entities;

    public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Text)
                .HasMaxLength(MaxDescriptionLength);

            builder.Property(f => f.Rating)
                .IsRequired();

            builder
                .HasOne(f => f.Place)
                .WithMany(f => f.Feedbacks)
                .HasForeignKey(f => f.PlaceId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
