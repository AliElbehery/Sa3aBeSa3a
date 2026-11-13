using Sa3aBeSa3a.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
            builder.Property(hr => hr.Status)
                .IsRequired();
            builder.HasOne(hr => hr.Member)
                .WithMany(m => m.HelpRequests)
                .HasForeignKey(hr => hr.MemberId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
