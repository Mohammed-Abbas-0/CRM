using CRM_Interface.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Application.Queries.Classes
{
    public class GetCampaignCommand : IRequest<CampaignRecord>
    {
        public int Id { get; set; }
    }
}
