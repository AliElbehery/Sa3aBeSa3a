using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sa3aBeSa3a.Domain.Entities;

namespace Sa3aBeSa3a.Infrastructure.Data.Configurations
{
    public class CategoryConfigurations : IEntityTypeConfiguration<Categories>
    {
        public void Configure(EntityTypeBuilder<Categories> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(c => c.Description)
                .HasMaxLength(500);
        }
    
    }
}
