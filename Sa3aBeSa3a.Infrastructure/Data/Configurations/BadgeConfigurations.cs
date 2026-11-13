using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sa3aBeSa3a.Domain.Entities;

namespace Sa3aBeSa3a.Infrastructure.Data.Configurations
{
    public class BadgeConfigurations : IEntityTypeConfiguration<Badges>
    {
        public void Configure(EntityTypeBuilder<Badges> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(b => b.Description)
                .HasMaxLength(500);
            builder.Property(b => b.Icon)
                .HasMaxLength(200);
        }
    
    }
}
