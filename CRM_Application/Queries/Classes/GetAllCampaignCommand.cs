using CRM_Interface.Dtos;
using MediatR;

namespace CRM_Application.Queries.Classes
{
    public class GetAllCampaignCommand:IRequest<List<CampaignRecord>>
    {
        public int? PageSize { get; set; } = 1;
        public int? PageCount { get; set; } = 20;
        public string? UserId {  get; set; }
    }
}
