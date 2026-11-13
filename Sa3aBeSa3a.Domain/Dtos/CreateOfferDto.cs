

namespace Sa3aBeSa3a.Domain.Dtos
{
    public class CreateOfferDto
    {
        public decimal OfferedHours { get; set; }
        public string? Message { get; set; } // Optional message from the member making the offer
    }
}
