
using Sa3aBeSa3a.Domain.Entities;
using Sa3aBeSa3a.Domain.Enums;

namespace Sa3aBeSa3a.Domain.Dtos
{
    public class CreateHelpRequestDto
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int CategoryId { get; set; }
        public decimal HoursNeeded { get; set; }
        public DateTime NeededBy { get; set; }
        public string District { get; set; } = null!;
    }
}
