using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM_Domain.Entities
{
    public class CampaignComment
    {
        [Key]
        public int Id { get; set; }

        // Foreign Keys
        [ForeignKey(nameof(Campaign))]
        public int CampaignId { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = default!;

        // Navigation Properties
        public Campaign Campaign { get; set; } = default!;
        public User User { get; set; } = default!;
        public int? Rating { get; set; } = 0;
        public string Content { get; set; } = default!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
