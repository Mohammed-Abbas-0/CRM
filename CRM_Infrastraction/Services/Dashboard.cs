using CRM_Domain.Entities;
using CRM_Infrastraction.Persistence;
using CRM_Interface.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Interface.Interfaces;
using System.Globalization;

namespace CRM_Infrastraction.Services
{
    public class Dashboard : IDashboard
    {
        private readonly AppDbContext _db;
        public Dashboard(AppDbContext db) => _db = db;

        public async Task<CampaignsInteractionDto> GetCampaignsInteractions(string companyId)
        {
            var _campaings = await _db.Campaigns.AsNoTracking().Where(idx => idx.UserId == companyId).Select(idx => idx.Id).ToListAsync();
            if (!_campaings.Any())
                return new CampaignsInteractionDto();
            //  Registrations Count
            var registrationCount = await _db.CampaignRegistrations
                                                .AsNoTracking()
                                                .Where(idx => _campaings.Contains(idx.CampaignId))
                                                .CountAsync();
            //  Likes Count
            var likesCount = await _db.CampaignInteractions
                                                .AsNoTracking()
                                                .Where(idx => _campaings.Contains(idx.CampaignId) && idx.InteractionType == CRM_Domain.Entities.InteractionType.Like)
                                                .CountAsync();

            //  Clients
            var clients = await _db.CampaignRegistrations
                                               .AsNoTracking()
                                               .Where(idx => _campaings.Contains(idx.CampaignId))
                                               .GroupBy(idx=> idx.UserId)
                                               .CountAsync();
            return new CampaignsInteractionDto(ClientCount: clients,LikesCount:likesCount,RegistrationsCount:registrationCount);
        }

        public async Task<List<CampaignsDataDto>> GetCampaingLikes(string companyId)
        {
            var campaigns = await _db.Campaigns
                                     .AsNoTracking()
                                     .Where(c => c.UserId == companyId)
                                     .ToListAsync();

            if (!campaigns.Any())
                return new List<CampaignsDataDto>();

            var campaignIds = campaigns.Select(c => c.Id).ToHashSet();
            var campaignDict = campaigns.ToDictionary(c => c.Id, c => c.Name);

            var interactions = await _db.CampaignInteractions
                                        .AsNoTracking()
                                        .Where(i => campaignIds.Contains(i.CampaignId) && i.InteractionType == CRM_Domain.Entities.InteractionType.Like)
                                        .GroupBy(i => i.CampaignId)
                                        .Select(g => new
                                        {
                                            CampaignId = g.Key,
                                            Count = g.Count()
                                        })
                                        .ToListAsync();

            var result = interactions.Select(i => new CampaignsDataDto(
                CampaingName: campaignDict.TryGetValue(i.CampaignId, out var name) ? name : "",
                Count: i.Count
            )).ToList();

            return result;
        }


        public async Task<List<CampaignsDataDto>> GetCampaingRegistration(string companyId)
        {
            var campaigns = await _db.Campaigns
                                      .AsNoTracking()
                                      .Where(c => c.UserId == companyId)
                                      .ToListAsync();

            if (!campaigns.Any())
                return new List<CampaignsDataDto>();

            var campaignIds = campaigns.Select(c => c.Id).ToHashSet();
            var campaignDict = campaigns.ToDictionary(c => c.Id, c => c.Name);

            var interactions = await _db.CampaignRegistrations
                                        .AsNoTracking()
                                        .Where(i => campaignIds.Contains(i.CampaignId))
                                        .GroupBy(i => i.CampaignId)
                                        .Select(g => new
                                        {
                                            CampaignId = g.Key,
                                            Count = g.Count()
                                        })
                                        .ToListAsync();

            var result = interactions.Select(i => new CampaignsDataDto(
                CampaingName: campaignDict.TryGetValue(i.CampaignId, out var name) ? name : "",
                Count: i.Count
            )).ToList();

            return result;
        }

        public async Task<List<ChartClientDto>> GetClientChart(string companyId)
        {
            var _campaigns = await _db.Campaigns
                                        .AsNoTracking()
                                        .Where(idx => idx.UserId == companyId)
                                        .ToListAsync();

            if (!_campaigns.Any())
                return new List<ChartClientDto>();

            var campaignIds = _campaigns.Select(idx => idx.Id).ToHashSet();

            var registrations = await _db.CampaignRegistrations
                                            .AsNoTracking()
                                            .Where(idx => campaignIds.Contains(idx.CampaignId))
                                            .ToListAsync();

            // 1. نختار أول تسجيل لكل مستخدم
            var firstRegistrationsPerUser = registrations
                .GroupBy(r => r.UserId)
                .Select(g => g.OrderBy(r => r.RegistrationDate).First())
                .ToList();

            // 2. نجمع المستخدمين حسب الشهر والسنة لأول تسجيل فقط
            var grouped = firstRegistrationsPerUser
                .GroupBy(r => new { r.RegistrationDate.Year, r.RegistrationDate.Month })
                .Select(g => new ChartClientDto
                (
                    MonthName: new DateTime(g.Key.Year, g.Key.Month, 1)
                                    .ToString("MMMM", new CultureInfo("ar-EG")),
                    ClientCount: g.Count()
                ))
                .OrderBy(g => g.MonthName)
                .ToList();

            return grouped;
        }

    }
}
