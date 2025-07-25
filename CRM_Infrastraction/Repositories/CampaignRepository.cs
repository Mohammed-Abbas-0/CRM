using CRM_Domain.Entities;
using CRM_Infrastraction.Persistence;
using CRM_Interface.Dtos;
using CRM_Interface.IRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CRM_Infrastraction.Repositories
{
    public class CampaignRepository : ICampaignRepository
    {
        private readonly AppDbContext _db;
        private readonly UserManager<User> _userManager;

        #region CTOR
        
        public CampaignRepository(AppDbContext db, UserManager<User> _userManage)
        {
            _userManager = _userManage;
            _db = db;
        }

        #endregion

        #region Public Methods

        #region Get BY ID

        public async Task<CampaignRecord> GetByIdAsync(int id)
        {
            var campaign = await _db.Campaigns.FindAsync(id);
            if (campaign is null)
                return new CampaignRecord(Name: "", Description: "", Budget: 0);

            return new CampaignRecord(
                Id: campaign.Id,
                Name: campaign.Name,
                Description: campaign.Description,
                Budget: campaign.Budget,
                StartDate: campaign.StartDate,
                EndDate: campaign.EndDate,
                ImageUrl: campaign.ImageUrl,
                ImageData: campaign.ImageData,
                BudgetAfterDiscount: campaign.BudgetAfterDiscount,
                RateDiscount: campaign.RateDiscount,
                UserId: campaign.UserId,
                UserName: _userManager.Users.FirstOrDefault(u => u.Id == campaign.UserId)?.FullName ?? "غير معروف"
                );
        }

        #endregion

        #region Save

        public async Task<bool> Save() => await _db.SaveChangesAsync() > 0 ? true : false;

        #endregion

        #region Add

        public async Task<bool> AddAsync(CampaignDto campaignDto)
        {
            var campaign = new Campaign
            {
                Description = campaignDto.Description,
                Name = campaignDto.Name,
                StartDate = campaignDto.StartDate,
                EndDate = campaignDto.EndDate,
                Budget = campaignDto.Budget,
                UserId = campaignDto.UserId,
                ImageUrl = campaignDto.ImageUrl,
                ImageData = campaignDto.ImageData,
                BudgetAfterDiscount = campaignDto.BudgetAfterDiscount,
                RateDiscount = campaignDto.RateDiscount
            };
            await _db.Campaigns.AddAsync(campaign);

            return await Save();
        }

        #endregion

        #region Change Campaign

        public async Task<bool> ChangeCampaignInteractionAsync(CampaignInteractionDto campaignInteractionDto)
        {
            var campaign = await _db.Campaigns.FindAsync(campaignInteractionDto.CampaignId);
            if (campaign is null)
                return false;

            RemoveAnyInteraction(campaignInteractionDto.CampaignId, campaignInteractionDto.UserId);

            CampaignInteraction campaignInteraction = new CampaignInteraction()
            {
                InteractionType = (InteractionType)campaignInteractionDto.InteractionType,
                UserId = campaignInteractionDto.UserId,
                CampaignId = campaignInteractionDto.CampaignId,
                CreatedAt = campaignInteractionDto.CreatedAt,
            };

            await _db.CampaignInteractions.AddAsync(campaignInteraction);


            bool result = await Save();
            return result;

        }

        #endregion

        #region Get Count

        // Get Count Of Camapaign Interactions
        public async Task<int> GetCampaignsLikesAsync(int campaignId)
        {
            var GetCampaignsLikes = await _db.CampaignInteractions
                    .AsNoTracking()
                    .CountAsync(i => i.CampaignId == campaignId && i.InteractionType == InteractionType.Like);
            return GetCampaignsLikes;
        }

        #endregion

        #region Register
        public async Task<ResponseMessage> RegisterCampaignAsync(RegisterCampaignDto registerCampaignDto)
        {
            var userExisted = await _userManager.FindByIdAsync(registerCampaignDto.UserId);
            if (userExisted is null)
                return new ResponseMessage { Message = "برجاء تسجل الدخول المحاولة مجددا", IsAuthenticated = false };

            var campaignExisted = await _db.Campaigns.FindAsync(registerCampaignDto.CampaignId);

            if (campaignExisted is null)
                return new ResponseMessage { Message = "برجاء المحاولة مجددا", IsAuthenticated = false };


            var campaignRegistration = new CampaignRegistration
            {
                UserId = registerCampaignDto.UserId,
                CampaignId = registerCampaignDto.CampaignId,
                RegistrationDate = DateTime.UtcNow,
            };
            _db.CampaignRegistrations.RemoveRange(_db.CampaignRegistrations.Where(idx => idx.CampaignId == registerCampaignDto.CampaignId && idx.UserId == registerCampaignDto.UserId));
            await _db.CampaignRegistrations.AddAsync(campaignRegistration);
            if (!await Save())
                return new ResponseMessage { Message = "برجاء المحاولة مجددا", IsAuthenticated = false };


            return new ResponseMessage { Message = "تم الاشتراك في الحملة", IsAuthenticated = true };

        }

        #endregion

        #region UnRegister

        public async Task<ResponseMessage> UnRegisterCampaignAsync(UnRegisterCampaignDto registerCampaignDto)
        {
            var userExisted = await _userManager.FindByIdAsync(registerCampaignDto.UserId);
            if (userExisted is null)
                return new ResponseMessage { Message = "برجاء تسجل الدخول المحاولة مجددا", IsAuthenticated = false };

            var campaignExisted = await _db.Campaigns.FindAsync(registerCampaignDto.CampaignId);

            if (campaignExisted is null)
                return new ResponseMessage { Message = "برجاء المحاولة مجددا", IsAuthenticated = false };

            _db.CampaignRegistrations.RemoveRange(_db.CampaignRegistrations.Where(idx => idx.CampaignId == registerCampaignDto.CampaignId && idx.UserId == registerCampaignDto.UserId));
            if (!await Save())
                return new ResponseMessage { Message = "برجاء المحاولة مجددا", IsAuthenticated = false };


            return new ResponseMessage { Message = "تم إلغاء الاشتراك في الحملة", IsAuthenticated = true };

        }

        #endregion

        #region Search

        public async Task<List<CampaignDto>> SearchCampaignAsync(string searchKeyWord)
        {
            var campaigns = await _db.Campaigns
                                        .AsNoTracking()
                                        .Where(idx => idx.Name.ToLower().Contains(searchKeyWord) && idx.EndDate > DateTime.UtcNow)
                                        .OrderByDescending(idx => idx.Id)
                                        .Take(10)
                                        .ToListAsync();
            if (campaigns.Any())
            {
                List<CampaignDto> campaignDtos = new List<CampaignDto>();
                foreach (var item in campaigns)
                {
                    CampaignDto campaignDto = new CampaignDto()
                    {
                        Id = item.Id,
                        Budget = item.Budget,
                        Description = item.Description,
                        StartDate = item.StartDate,
                        EndDate = item.EndDate,
                        Name = item.Name,
                        UserId = item.UserId
                    };
                    campaignDtos.Add(campaignDto);
                }
                return campaignDtos;
            }

            return new List<CampaignDto>();
        }

        #endregion

        #region Update

        public async Task<bool> UpdateAsync(int id, CampaignDto campaignDto)
        {
            var campaign = await _db.Campaigns.FindAsync(id);
            if (campaign is null) return false;
            campaign.Description = campaignDto.Description;
            campaign.Name = campaignDto.Name;
            campaign.StartDate = campaignDto.StartDate;
            campaign.EndDate = campaignDto.EndDate;
            campaign.Budget = campaignDto.Budget;

            return await Save();
        }

        #endregion

        #region Get All

        public async Task<List<CampaignRecord>> GetAllCamaigns(CampaignsDto campaignsDto)
        {
            bool filterByUserId = !string.IsNullOrEmpty(campaignsDto.UserId);

            var campaigns = await _db.Campaigns
                                    .AsNoTracking()
                                    .Where(idx => idx.EndDate > DateTime.UtcNow)
                                    .OrderByDescending(idx => idx.Id)
                                    .Skip(((campaignsDto.PageSize - 1) * campaignsDto.PageCount) ?? 0)
                                    .Take(campaignsDto.PageCount ?? 0)
                                    .ToListAsync();
            if (!campaigns.Any())
                return new List<CampaignRecord>();




            List<CampaignRecord> campaignRecords = new List<CampaignRecord>();
            foreach (var campaign in campaigns)
            {
                int likesCount = await GetCampaignsLikesAsync(campaign.Id);
                bool isLiked = false;

                if (!filterByUserId)
                {
                    campaignRecords.Add(new CampaignRecord(
                        Id: campaign.Id,
                        Name: campaign.Name,
                        Description: campaign.Description,
                        StartDate: campaign.StartDate,
                        EndDate: campaign.EndDate,
                        Budget: campaign.Budget,
                        IsRegistered: false,
                        LikesCount: likesCount,
                        IsLiked: isLiked,
                        ImageUrl: campaign.ImageUrl,
                        ImageData: campaign.ImageData,
                        BudgetAfterDiscount: campaign.BudgetAfterDiscount,
                        RateDiscount: campaign.RateDiscount,
                        UserId: campaign.UserId,
                        UserName: _userManager.Users.FirstOrDefault(u => u.Id == campaign.UserId)?.FullName ?? "غير معروف"
                        ));
                }
                else
                {
                    isLiked = await CampaignLikedByUser(campaign.Id, campaignsDto.UserId);
                    campaignRecords.Add(new CampaignRecord(
                       Id: campaign.Id,
                       Name: campaign.Name,
                       Description: campaign.Description,
                       StartDate: campaign.StartDate,
                       EndDate: campaign.EndDate,
                       Budget: campaign.Budget,
                       IsRegistered: await IsCampaignRegistration(campaign.Id, campaignsDto.UserId ?? ""),
                       LikesCount: likesCount,
                       IsLiked: isLiked,
                       ImageUrl: campaign.ImageUrl,
                       ImageData: campaign.ImageData,
                       BudgetAfterDiscount: campaign.BudgetAfterDiscount,
                        RateDiscount: campaign.RateDiscount,
                        UserId: campaign.UserId,
                        UserName: _userManager.Users.FirstOrDefault(u => u.Id == campaign.UserId)?.FullName ?? "غير معروف"

                       ));
                }
            }
            return campaignRecords;
        }

        #endregion

        #region Add Comment

        public async Task<bool> AddCommentAsync(CampaignCommentDto commentsDto)
        {
            CampaignComment comment = new CampaignComment
            {
                Content = commentsDto.Content,
                UserId = commentsDto.UserId,
                CampaignId = commentsDto.CampaignId,
                CreatedAt = DateTime.Now,
                Rating = commentsDto.Rating
            };
            _db.CampaignComments.Add(comment);
            return await Save();
        }

        #endregion

        #region Get All Comments

        public async Task<List<CampaignCommentRecord>> GetCampaignCommentsAsync(int campaignId)
        {
            var campaignComments = await _db.CampaignComments.AsNoTracking().Where(idx => idx.CampaignId == campaignId).OrderByDescending(idx => idx.Id).ToListAsync();
            if (!campaignComments.Any())
                return new List<CampaignCommentRecord>();

            List<CampaignCommentRecord> campaignCommentRecords = new();
            foreach(var comment in campaignComments)
            {
                var userName = await _db.Users.Where(idx=> idx.Id == comment.UserId).Select(idx => idx.FullName).FirstOrDefaultAsync();
                var campaignCommentRecord = new CampaignCommentRecord
                (
                    Content: comment.Content,
                    UserId: comment.UserId,
                    UserName: userName??"",
                    CreatedAt: comment.CreatedAt.ToString("yyyy-MM-dd hh:mm"),
                    Rating: comment.Rating
                );
                campaignCommentRecords.Add(campaignCommentRecord);
            }

            return campaignCommentRecords;

        }

        #endregion

        #endregion

        #region Private Methods

        private async Task<bool> IsCampaignRegistration(int campaignId, string userId)
        {
            var campaignRegistrations = await _db.CampaignRegistrations
                                        .AsNoTracking()
                                        .OrderByDescending(idx => idx.Id)
                                        .FirstOrDefaultAsync(idx => idx.UserId == userId && idx.CampaignId == campaignId);
            if (campaignRegistrations is null)
                return false;



            return true;

        }

        private async Task<bool> CampaignLikedByUser(int campaignId, string userId)
        {
            var isLiked = await _db.CampaignInteractions.AnyAsync(idx => idx.CampaignId == campaignId && idx.UserId == userId && idx.InteractionType == InteractionType.Like);
            return isLiked;
        }

        // Remove Any Interaction that create by user
        private void RemoveAnyInteraction(int campaignId, string userId)
        {
            var existing = _db.CampaignInteractions
                    .Where(i => i.CampaignId == campaignId && i.UserId == userId);

            _db.CampaignInteractions.RemoveRange(existing);
        }

        #endregion
    }
}
