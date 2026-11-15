

using Sa3aBeSa3a.Domain.Entities;
using Sa3aBeSa3a.Domain.Enums;

namespace Sa3aBeSa3a.Domain.Dtos
{
    public class RegisterDto
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }
}
