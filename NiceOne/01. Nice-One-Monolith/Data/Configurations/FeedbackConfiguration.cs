namespace NiceOne.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    
    using NiceOne.Data.Constants;
    using NiceOne.Data.Entities;

    public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Text)
                .HasMaxLength(DataConstants.MaxDescriptionLength);

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
