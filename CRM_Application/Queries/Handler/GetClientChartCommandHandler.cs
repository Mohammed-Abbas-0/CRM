using CRM_Application.Queries.Classes;
using CRM_Interface.Dtos;
using CRM_Interface.Interfaces;
using MediatR;

namespace CRM_Application.Queries.Handler
{
    public class GetClientChartCommandHandler : IRequestHandler<GetClientChartCommand, List<ChartClientDto>>
    {
        private readonly IDashboard _dashboard;
        public GetClientChartCommandHandler(IDashboard dashboard) => _dashboard = dashboard;    

        public async Task<List<ChartClientDto>> Handle(GetClientChartCommand request, CancellationToken cancellationToken)
        {
            var response = await _dashboard.GetClientChart(request.CompanyId);
            return response;
        }
    }
}
