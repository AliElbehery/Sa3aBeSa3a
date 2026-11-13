using Sa3aBeSa3a.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sa3aBeSa3a.Infrastructure.Data.Configurations
{
    public class MemberConfigurations : IEntityTypeConfiguration<Members>
    {
        public void Configure(EntityTypeBuilder<Members> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(m => m.Email)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(m => m.PhoneNumber)
                .IsRequired()
                .HasMaxLength(15);
            builder.Property(m => m.PasswordHash)
                .IsRequired()
                .HasMaxLength(256);
            // Configure relationships
            builder.HasMany(m => m.LedgerEntries)
                .WithOne(le => le.Member)
                .HasForeignKey(le => le.MemberId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(m => m.MemberSkills)
                .WithOne(ms => ms.Member)
                .HasForeignKey(ms => ms.MemberId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(m => m.MemberBadges)
                .WithOne(b => b.Member)
                .HasForeignKey(b => b.MemberId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasMany(m => m.HelpRequests)
                .WithOne(hr => hr.Member)
                .HasForeignKey(hr => hr.MemberId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(m => m.Offers)
                .WithOne(o => o.Member)
                .HasForeignKey(o => o.MemberId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
