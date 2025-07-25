using CRM_Application.Queries.Classes;
using CRM_Interface.Dtos;
using CRM_Interface.Interfaces;
using CRM_Interface.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Application.Queries.Handler
{
  
    public class GetSearchCampaignCommandHandler : IRequestHandler<GetSearchCampaignCommand, List<CampaignDto>>
    {
        private readonly ICampaignRepository _campaignRepository;

        public GetSearchCampaignCommandHandler(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        public async Task<List<CampaignDto>> Handle(GetSearchCampaignCommand request, CancellationToken cancellationToken)
        {
            var response = await _campaignRepository.SearchCampaignAsync(request.SearchKeyWord);
            return response;
        }


    }
}
