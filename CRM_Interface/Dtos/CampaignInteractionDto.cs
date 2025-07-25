using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Interface.Dtos
{
    public class CampaignInteractionDto
    { 
        public int Id { get; set; }
        public required string UserId { get; set; }
        public required int CampaignId { get; set; }
        public int InteractionType { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    
}
