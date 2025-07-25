using CRM_Application.Queries.Classes;
using CRM_Interface.Dtos;
using CRM_Interface.IRepositories;
using MediatR;

namespace CRM_Application.Queries.Handler
{
    public class GetAllCampaignCommentsHandler : IRequestHandler<GetAllCampaignCommentsCommand, List<CampaignCommentRecord>>
    {
        private readonly ICampaignRepository _campaignRepository;

        public GetAllCampaignCommentsHandler(ICampaignRepository campaignRepository) => _campaignRepository = campaignRepository;
        
        public async Task<List<CampaignCommentRecord>> Handle(GetAllCampaignCommentsCommand request, CancellationToken cancellationToken)
            => await _campaignRepository.GetCampaignCommentsAsync(request.CampaignId);
        
    }
}
