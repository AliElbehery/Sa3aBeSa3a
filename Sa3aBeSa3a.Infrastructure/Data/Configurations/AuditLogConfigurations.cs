using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sa3aBeSa3a.Domain.Entities;

namespace Sa3aBeSa3a.Infrastructure.Data.Configurations
{
    internal class AuditLogConfigurations : IEntityTypeConfiguration<AuditLogs>
    {
        public void Configure(EntityTypeBuilder<AuditLogs> builder)
        {
            builder.HasKey(al => al.Id);
            builder.Property(al => al.Action)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(al => al.Source)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(al => al.Target)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(al => al.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("SYSUTCDATETIME()");
            builder.Property(al => al.Actor)
                   .IsRequired()
                   .HasMaxLength(100);

        }
    
    }
}
