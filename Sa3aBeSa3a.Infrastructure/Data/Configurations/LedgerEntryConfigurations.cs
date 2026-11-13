using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sa3aBeSa3a.Domain.Entities;
using Sa3aBeSa3a.Domain.Enums;

namespace Sa3aBeSa3a.Infrastructure.Data.Configurations
{
    public class LedgerEntryConfigurations : IEntityTypeConfiguration<LedgerEntries>
    {
        public void Configure(EntityTypeBuilder<LedgerEntries> builder)
        {
            builder.HasKey(le => le.Id);
            builder.Property(le => le.Amount)
                .IsRequired()
                .HasPrecision(10, 2);
            builder.Property(le => le.TransactionType)
                .IsRequired()
                .HasMaxLength(20)
                .HasConversion(
                    v => v.ToString(),
                    v => (TransactionTypes)Enum.Parse(typeof(TransactionTypes), v));
            builder.ToTable("LedgerEntries", table =>
            {
                table.HasCheckConstraint(
                    "CK_LedgerEntries_Amount",
                    """
                    (
                        [TransactionType] IN ('WelcomeBonus', 'Credit')
                          AND [Amount] > 0
                    )
                    OR
                    (
                        [TransactionType] = 'Debit'
                        AND [Amount] < 0
                    )
                    """
                );
            });
            builder.HasOne(le => le.Member)
                .WithMany(m => m.LedgerEntries)
                .HasForeignKey(le => le.MemberId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Property(le => le.Date)
                            .IsRequired()
                            .HasDefaultValueSql("SYSUTCDATETIME()");
        }
    }
    
    
}
