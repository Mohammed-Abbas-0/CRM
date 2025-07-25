using CRM_Application.Queries.Classes;
using CRM_Interface.Dtos;
using CRM_Interface.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Application.Queries.Handler
{
    public class IGetCampaignCommandHandler : IRequestHandler<GetCampaignCommand, CampaignRecord>
    {
        private readonly ICampaignRepository _campaignRepository;
        public IGetCampaignCommandHandler(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }
        public async Task<CampaignRecord> Handle(GetCampaignCommand request, CancellationToken cancellationToken)
        {
            var campaign = await _campaignRepository.GetByIdAsync(request.Id);
            

            return campaign;
        }
    }
}
