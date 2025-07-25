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
    public class GetCampaignsInteractionCommandHandler : IRequestHandler<GetCampaignsInteractionCommand, CampaignsInteractionDto>
    {
        private readonly IDashboard _dashboard;
        public GetCampaignsInteractionCommandHandler(IDashboard dashboard) => _dashboard = dashboard;
        public async Task<CampaignsInteractionDto> Handle(GetCampaignsInteractionCommand request, CancellationToken cancellationToken)
        {
            var response = await _dashboard.GetCampaignsInteractions(request.CompanyId);
            return response;
        }
    }
}
