using Sa3aBeSa3a.Domain.Entities;
using Sa3aBeSa3a.Domain.Enums;
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
                .IsRequired();
            // Configure relationships
            builder.HasMany(m => m.LedgerEntries)
                .WithOne(le => le.Member)
                .HasForeignKey(le => le.MemberId);
            builder.HasMany(m => m.MemberSkills)
                .WithOne(ms => ms.Member)
                 .HasForeignKey(ms => ms.MemberId);
            builder.HasMany(m => m.MemberBadges)
                .WithOne(b => b.Member)
                .HasForeignKey(b => b.MemberId);
            builder.HasIndex(x => x.Email).IsUnique();
        }
    }
}
