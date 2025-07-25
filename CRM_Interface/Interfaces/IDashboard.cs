using CRM_Interface.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Interface.Interfaces
{
    public interface IDashboard
    {
        Task<List<CampaignsDataDto>> GetCampaingLikes (string companyId);
        Task<List<CampaignsDataDto>> GetCampaingRegistration (string companyId);
        Task<CampaignsInteractionDto> GetCampaignsInteractions (string companyId);
        Task<List<ChartClientDto>> GetClientChart(string companyId);
    }
}
