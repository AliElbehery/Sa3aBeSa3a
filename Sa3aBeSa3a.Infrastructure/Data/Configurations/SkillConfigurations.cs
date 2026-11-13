using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sa3aBeSa3a.Domain.Entities;

namespace Sa3aBeSa3a.Infrastructure.Data.Configurations
{
    public class SkillConfigurations : IEntityTypeConfiguration<Skills>
    {
        public void Configure(EntityTypeBuilder<Skills> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.HasOne(s => s.Category)
                .WithMany(c => c.Skills)
                .HasForeignKey(s => s.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
