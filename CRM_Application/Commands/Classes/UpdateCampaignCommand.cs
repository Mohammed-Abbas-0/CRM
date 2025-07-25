using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Application.Commands.Classes
{
    public class UpdateCampaignCommand: IRequest<bool>
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Budget { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
