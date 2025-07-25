using CRM_Application.Queries.Classes;
using CRM_Interface.Dtos;
using CRM_Interface.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Application.Queries.Handler
{
    public class GetCampaignsLikesCommandHandler:IRequestHandler<GetCampaignsLikesCommand, List<CampaignsDataDto>>
    {
        private readonly IDashboard _dashboard;
        public GetCampaignsLikesCommandHandler(IDashboard dashboard) => _dashboard = dashboard;

        public async Task<List<CampaignsDataDto>> Handle(GetCampaignsLikesCommand request, CancellationToken cancellationToken)
        {
            var response = await _dashboard.GetCampaingLikes(request.CompanyId);
            return response;
        }
    }
}
