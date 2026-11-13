using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sa3aBeSa3a.Domain.Entities;

namespace Sa3aBeSa3a.Infrastructure.Data.Configurations
{
    public class ReviewConfigurations : IEntityTypeConfiguration<Reviews>
    {
        public void Configure(EntityTypeBuilder<Reviews> builder)
        {
            builder.HasKey(r => r.Id);
            builder.HasOne(r => r.Reviewer)
                .WithMany()
                .HasForeignKey(r => r.ReviewerId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(r => r.Reviewee)
                .WithMany()
                .HasForeignKey(r => r.RevieweeId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Property(r => r.Comment)
                .HasMaxLength(1000);
            builder.Property(r => r.Rating)
                .IsRequired();
            builder.ToTable("Reviews", table =>
            {
                table.HasCheckConstraint(
                    "CK_Rev_Rating",
                    "[Rating] >= 1 AND [Rating] <= 5"
                );
            });
            builder.HasIndex(r => new { r.ReviewerId, r.RevieweeId, r.HelpRequestId }).IsUnique();
        }
    
    }
}
