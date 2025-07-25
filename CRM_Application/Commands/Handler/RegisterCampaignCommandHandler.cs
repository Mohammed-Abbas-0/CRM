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
    public class RegisterCampaignCommandHandler : IRequestHandler<RegisterCampaignCommand, ResponseMessage>
    {
        private readonly ICampaignRepository _campaignRepository;
        public RegisterCampaignCommandHandler(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }
        public async Task<ResponseMessage> Handle(RegisterCampaignCommand registerCampaign, CancellationToken cancellationToken)
        {
            var registerCampaignDto = new RegisterCampaignDto { CampaignId = registerCampaign.CampaignId,UserId = registerCampaign.UserId };
            var request = await _campaignRepository.RegisterCampaignAsync(registerCampaignDto);
            return request;
        }
    }
}