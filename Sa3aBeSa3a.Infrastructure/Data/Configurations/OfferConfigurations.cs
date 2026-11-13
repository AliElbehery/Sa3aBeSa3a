using Sa3aBeSa3a.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sa3aBeSa3a.Domain.Enums;

namespace Sa3aBeSa3a.Infrastructure.Data.Configurations
{
    public class OfferConfigurations : IEntityTypeConfiguration<Offers>

    {
        public void Configure(EntityTypeBuilder<Offers> builder)
        {
            builder.Property(o => o.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (OfferStatus)Enum.Parse(typeof(OfferStatus), v));
        }
    }
}
