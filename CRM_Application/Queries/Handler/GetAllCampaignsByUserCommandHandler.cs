using CRM_Application.Queries.Classes;
using CRM_Interface.Dtos;
using CRM_Interface.IRepositories;
using MediatR;

namespace CRM_Application.Queries.Handler
{
    internal class GetAllCampaignsByUserCommandHandler : IRequestHandler<GetAllCampaignsByUserCommand, List<CampaignRecord>>
    {
        private readonly ICampaignRepository _campaignRepository;

        public GetAllCampaignsByUserCommandHandler(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        public async Task<List<CampaignRecord>> Handle(GetAllCampaignsByUserCommand request, CancellationToken cancellationToken)
            => await _campaignRepository.GetAllCamaignsByUserId(request.UsertId);
    }
}
