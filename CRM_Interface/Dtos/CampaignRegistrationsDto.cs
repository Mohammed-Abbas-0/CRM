using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Interface.Dtos
{
    public class CampaignRegistrationsDto
    {
        public required string UserId { get; set; }
        public required List<int>  CamapignIds { get; set; }
        public int  CountCamapigns { get; set; }
    }
}
