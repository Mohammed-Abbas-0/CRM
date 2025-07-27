using CRM_Interface.Dtos;
using MediatR;

namespace CRM_Application.Queries.Classes
{
    public class GetAllCampaignsByUserCommand:IRequest<List<CampaignRecord>>
    {
        public required string UsertId { get; set; }
    }
}
