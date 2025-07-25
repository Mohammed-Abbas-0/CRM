using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Interface.Dtos
{
    public class CampaignsDto
    {
        public int? PageSize { get; set; } = 1;
        public int? PageCount { get; set; } = 50;
        public string? UserId { get; set; }
    }
    
}
