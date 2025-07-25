using CRM_Interface.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Interface.IRepositories
{
    public interface ICampaignRepository
    {
        Task<CampaignRecord> GetByIdAsync(int id);
        Task<List<CampaignRecord>> GetAllCamaigns(CampaignsDto campaignsDto);
        Task<bool> AddAsync(CampaignDto campaign);
        Task<bool> UpdateAsync(int id,CampaignDto campaign);
        Task<bool> ChangeCampaignInteractionAsync(CampaignInteractionDto campaignInteractionDto);
        Task<ResponseMessage> RegisterCampaignAsync(RegisterCampaignDto registerCampaignDto);
        Task<ResponseMessage> UnRegisterCampaignAsync(UnRegisterCampaignDto registerCampaignDto);
        Task<int> GetCampaignsLikesAsync(int campaignId);
        Task<List<CampaignDto>> SearchCampaignAsync(string searchKeyWord);
        Task<bool> Save();

    }

}
