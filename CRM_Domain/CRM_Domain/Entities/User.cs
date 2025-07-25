using Microsoft.AspNetCore.Identity;

namespace CRM_Domain.Entities
{
    public class User : IdentityUser
    {
        public required string FullName { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public required string Address { get; set; }
        public required int AccountType { get; set; } // 1. User   2. Company
        public string? TaxNo { get; set; }
    }
}
