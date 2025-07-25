using CRM_Application.Commands.Classes;
using CRM_Interface.Dtos;
using CRM_Interface.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Application.Commands.Handler
{
    public class UnRegisterCampaignCommandHandler : IRequestHandler<UnRegisterCampaignCommand, ResponseMessage>
    {
        private readonly ICampaignRepository _campaignRepository;
        public UnRegisterCampaignCommandHandler(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }
        public async Task<ResponseMessage> Handle(UnRegisterCampaignCommand registerCampaign, CancellationToken cancellationToken)
        {
            var registerCampaignDto = new UnRegisterCampaignDto { CampaignId = registerCampaign.CampaignId,UserId = registerCampaign.UserId };
            var request = await _campaignRepository.UnRegisterCampaignAsync(registerCampaignDto);
            return request;
        }
    }
}