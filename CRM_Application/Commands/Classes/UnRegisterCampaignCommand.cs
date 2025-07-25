using CRM_Interface.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Application.Commands.Classes
{
    public class UnRegisterCampaignCommand : IRequest<ResponseMessage>
    {
        public required string UserId {  get; set; }    
        public required int CampaignId {  get; set; }    
    }
}
