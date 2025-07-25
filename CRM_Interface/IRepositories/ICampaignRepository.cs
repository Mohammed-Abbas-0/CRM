using CRM_Interface.Dtos;

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
        Task<List<CampaignCommentRecord>> GetCampaignCommentsAsync(int campaignId);
        Task<bool> AddCommentAsync(CampaignCommentDto commentsDto);
        Task<bool> Save();

    }

}
