using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sa3aBeSa3a.Domain.Entities;
using Sa3aBeSa3a.Domain.Enums;

namespace Sa3aBeSa3a.Infrastructure.Data.Configurations
{
    internal class HelpRequestConfigurations : IEntityTypeConfiguration<HelpRequests>
    {
        public void Configure(EntityTypeBuilder<HelpRequests> builder)
        {
            builder.HasKey(hr => hr.Id);
            builder.Property(hr => hr.Title)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(hr => hr.Description)
                .IsRequired()
                .HasMaxLength(500);
            builder.HasOne(hr => hr.Member)
                .WithMany(m => m.HelpRequests)
                .HasForeignKey(hr => hr.MemberId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Property(hr => hr.HoursNeeded)
                .IsRequired()
                .HasPrecision(4, 2);
            builder.Property(hr => hr.RequestDate)
                .IsRequired();
            builder.Property(hr => hr.NeededBy)
                .IsRequired();
            builder.Property(hr => hr.District)
                .IsRequired()
                .HasMaxLength(100);
            builder.HasOne(hr => hr.Category)
                   .WithMany()
                   .HasForeignKey(hr => hr.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.Property(hr => hr.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (HelpRequestStatus)Enum.Parse(typeof(HelpRequestStatus), v));
            builder.ToTable("HelpRequests", table =>
            {
                table.HasCheckConstraint(
                    "CK_HelpRequests_HoursNeeded",
                    "[HoursNeeded] >= 0.5 AND [HoursNeeded] <= 8 AND [HoursNeeded] * 2 = FLOOR([HoursNeeded] * 2)"
                );
            });
        }
    }
}
