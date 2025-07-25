using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Interface.Dtos
{
    public class RegisterCampaignDto
    {
        public required string UserId { get; set; }
        public required int CampaignId { get; set; }
    }
}
