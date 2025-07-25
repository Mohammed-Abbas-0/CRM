using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Application.Commands.Classes
{
    public class ChangeCampaignInteractionCommand:IRequest<bool>
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public int CampaignId { get; set; }
        public int InteractionType { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
