using CRM_Domain.Entities;
using CRM_Infrastraction.Persistence;
using CRM_Interface.Dtos;
using CRM_Interface.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Infrastraction.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext _db;
        private readonly UserManager<User> _userManager;
        public ClientRepository(AppDbContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public async Task<List<ClientDto>> GetClientsAsync(string CompanyId)
        {
            List<ClientDto> clientDtos = new List<ClientDto>();
            List<int> campaignIds = await _db.Campaigns.AsNoTracking().Where(idx => idx.UserId == CompanyId).Select(idx=> idx.Id ).ToListAsync();

            var campaignRegistrationsDetails = await _db.CampaignRegistrations
                                .AsNoTracking()
                                .Where(idx=> campaignIds.Contains(idx.CampaignId))
                                .GroupBy(idx => idx.UserId)
                                .Select(idx=> new CampaignRegistrationsDto
                                {
                                    UserId = idx.Key,
                                    CamapignIds = idx.Select(col=>col.CampaignId).ToList(),
                                    CountCamapigns = idx.Select(col=>col.CampaignId).Count()
                                })
                                .ToListAsync();

            if(campaignRegistrationsDetails.Any() && campaignRegistrationsDetails.Count() > 0)
            {
                foreach(var camapign in campaignRegistrationsDetails)
                {
                    var camapignDtl = await _db.Campaigns
                                                        .AsNoTracking()
                                                        .Where(idx=> camapign.CamapignIds.Contains(idx.Id))
                                                        .Select(idx=>idx.Name)
                                                        .ToListAsync();

                    var UserDtl = await _userManager.FindByIdAsync(camapign.UserId);

                    ClientDto clientDto = new ClientDto(
                        UserId: camapign.UserId,
                        UserName: UserDtl != null ? UserDtl.FullName: "",
                        CamapignCount:camapign.CountCamapigns,
                        CamapignName: camapignDtl,
                        PhoneNumber: UserDtl != null ? UserDtl.PhoneNumber??"":"",
                        Address: UserDtl != null ? UserDtl.Address:""
                        );

                    
                    clientDtos.Add( clientDto );
                }
            }
            return clientDtos;

        }
    }
}
