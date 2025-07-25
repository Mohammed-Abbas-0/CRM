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
    public class IGetAllCampaignCommandHandler:IRequestHandler<GetAllCampaignCommand, List<CampaignRecord>>
    {
        private readonly ICampaignRepository _campaignRepository;
        public IGetAllCampaignCommandHandler(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }
        public async Task<List<CampaignRecord>> Handle(GetAllCampaignCommand request, CancellationToken cancellationToken)
        {
            var campaignsDto = new CampaignsDto 
            { 
                PageCount = request.PageCount,
                PageSize = request.PageSize,
                UserId = request.UserId,
            };
            var campaigns = await _campaignRepository.GetAllCamaigns(campaignsDto);
            

            return campaigns;
        }
    }
}
