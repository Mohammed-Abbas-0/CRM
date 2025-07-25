using CRM_Interface.Dtos;
using MediatR;

namespace CRM_Application.Queries.Classes
{
    public class GetAllCampaignCommentsCommand:IRequest<List<CampaignCommentRecord>>
    {
        public int CampaignId { get; set; }
    }
}
