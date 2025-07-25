using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Domain.Entities
{
    public class Interaction
    {
        public int Id { get; set; } 

        public required int CustomerId { get; set; } // العميل اللي اتفاعل
        public Customer Customer { get; set; } // Navigation Property

        public required int CampaignId { get; set; } // الحملة اللي اتفاعل معاها
        public Campaign Campaign { get; set; } // Navigation Property

        public required string ActionType { get; set; } // نوع التفاعل (مثلاً: Like - Register - Purchase)

        public DateTime ActionDate { get; set; } = DateTime.Now; // وقت التفاعل
    }

}
