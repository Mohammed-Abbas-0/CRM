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
    public class GetCampaignsRegisterationCommandHandler : IRequestHandler<GetCampaignsRegisterationCommand, List<CampaignsDataDto>>
    {
        private readonly IDashboard _dashboard;
        public GetCampaignsRegisterationCommandHandler(IDashboard dashboard) => _dashboard = dashboard;

        public async Task<List<CampaignsDataDto>> Handle(GetCampaignsRegisterationCommand request, CancellationToken cancellationToken)
        {
            var response = await _dashboard.GetCampaingRegistration(request.CompanyId);
            return response;
        }
    }
}
