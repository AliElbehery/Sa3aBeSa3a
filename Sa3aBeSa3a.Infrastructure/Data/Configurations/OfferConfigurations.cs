using Sa3aBeSa3a.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sa3aBeSa3a.Domain.Enums;

namespace Sa3aBeSa3a.Infrastructure.Data.Configurations
{
    public class OfferConfigurations : IEntityTypeConfiguration<Offers>

    {
        public void Configure(EntityTypeBuilder<Offers> builder)
        {
            builder.Property(o => o.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (OfferStatus)Enum.Parse(typeof(OfferStatus), v))
                .IsRequired();
            builder.Property(o => o.OfferedHours)
                .IsRequired()
                .HasPrecision(6, 2);
            builder.ToTable("Offers", table =>
            {
                table.HasCheckConstraint(
                    "CK_Offers_OfferedHours",
                    "[OfferedHours] > 0 AND [OfferedHours] * 2 = FLOOR([OfferedHours] * 2)"
                );
            });
            builder.Property(o => o.OfferDate)
                .IsRequired();
            builder.HasOne(o => o.HelpRequest)
                .WithMany(hr => hr.Offers)
                .HasForeignKey(o => o.HelpRequestId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasKey(o => o.Id);
            builder.HasOne(o => o.Member)
                .WithMany(m => m.Offers)
                .HasForeignKey(o => o.MemberId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Property(o => o.Message)
                .HasMaxLength(500);
            builder.HasIndex(o => new { o.HelpRequestId, o.MemberId }).IsUnique();

        }
    }
}
