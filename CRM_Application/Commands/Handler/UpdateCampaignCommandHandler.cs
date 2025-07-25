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
    public class UpdateCampaignCommandHandler : IRequestHandler<UpdateCampaignCommand, bool>
    {
        private readonly ICampaignRepository _campaignRepository;
        public UpdateCampaignCommandHandler(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }
        public async Task<bool> Handle(UpdateCampaignCommand request, CancellationToken cancellationToken)
        {
            var campaignDto = new CampaignDto()
            {
                Description = request.Description??"",
                Name = request.Name??"",
                StartDate = request.StartDate ?? DateTime.UtcNow,
                EndDate = request.EndDate?? DateTime.UtcNow,
                Budget = request.Budget??0,
                UserId = request.UserId
            };

            var result = await _campaignRepository.UpdateAsync(request.Id,campaignDto);

            return result;
        }
    }
}
