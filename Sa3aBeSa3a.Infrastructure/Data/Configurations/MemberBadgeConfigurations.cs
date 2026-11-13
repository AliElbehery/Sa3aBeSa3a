using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sa3aBeSa3a.Domain.Entities;

namespace Sa3aBeSa3a.Infrastructure.Data.Configurations
{
    public class MemberBadgeConfigurations : IEntityTypeConfiguration<MemberBadges>
    {
        public void Configure(EntityTypeBuilder<MemberBadges> builder)
        {
            builder.HasKey(mb => new { mb.MemberId, mb.BadgeId });
            builder.HasOne(mb => mb.Member)
                .WithMany(m => m.MemberBadges)
                .HasForeignKey(mb => mb.MemberId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(mb => mb.Badge)
                .WithMany()
                .HasForeignKey(mb => mb.BadgeId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasIndex(mb => new { mb.MemberId, mb.BadgeId }).IsUnique();
        }
    
    }
}
