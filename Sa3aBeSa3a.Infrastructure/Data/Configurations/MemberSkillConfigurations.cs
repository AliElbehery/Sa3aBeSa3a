using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sa3aBeSa3a.Domain.Entities;

namespace Sa3aBeSa3a.Infrastructure.Data.Configurations
{
    public class MemberSkillConfigurations : IEntityTypeConfiguration<MemberSkills>
    {
        public void Configure(EntityTypeBuilder<MemberSkills> builder)
        {
            builder.HasKey(ms => new { ms.MemberId, ms.SkillId });

            builder.HasOne(ms => ms.Member)
                .WithMany(m => m.MemberSkills)
                .HasForeignKey(ms => ms.MemberId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(ms => ms.Skill)
                .WithMany()
                .HasForeignKey(ms => ms.SkillId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasIndex(ms => new { ms.MemberId, ms.SkillId }).IsUnique();
        }
    }
}
