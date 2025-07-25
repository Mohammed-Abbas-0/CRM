using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Domain.Entities
{
    public enum InteractionType
    {
        Like = 1,
        Unlike = 2
    }

    public class CampaignInteraction
    {
        [Key]
        public int Id { get; set; }
        public required string UserId { get; set; }
        public required int CampaignId { get; set; }
        public InteractionType InteractionType { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public User User { get; set; }
        public Campaign Campaign { get; set; }
    }


}
