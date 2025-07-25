using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Interface.Dtos
{
    public record CampaignsInteractionDto
    (
         int RegistrationsCount = 0,
         int LikesCount = 0,
         int ClientCount = 0
    );
}
