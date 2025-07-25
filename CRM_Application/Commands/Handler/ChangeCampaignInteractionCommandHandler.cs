using CRM_Application.Commands.Classes;
using CRM_Interface.Dtos;
using CRM_Interface.Interfaces;
using CRM_Interface.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Application.Commands.Handler
{
    public class ChangeCampaignInteractionCommandHandler : IRequestHandler<ChangeCampaignInteractionCommand, bool>
    {
        private readonly ICampaignRepository _campaignRepository;
        public ChangeCampaignInteractionCommandHandler(ICampaignRepository campaignRepository) => _campaignRepository = campaignRepository;
        
        public async Task<bool> Handle(ChangeCampaignInteractionCommand request, CancellationToken cancellationToken)
        {
            var campaignInteractionDto = new CampaignInteractionDto()
            {
                CampaignId = request.CampaignId,
                UserId = request.UserId,
                CreatedAt = DateTime.UtcNow,
                InteractionType = request.InteractionType,
            };

            var result = await _campaignRepository.ChangeCampaignInteractionAsync(campaignInteractionDto);
            return result;

            

        }
    }
}
