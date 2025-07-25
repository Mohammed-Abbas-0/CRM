using MediatR;

namespace CRM_Application.Commands.Classes
{
    public class AddCampaignCommentCommand:IRequest<bool>
    {
        public int CampaignId { get; set; }
        public required string UserId { get; set; }
        public required string Content {get; set; }
        public int? Rating { get; set; } = 0;

    }
}
