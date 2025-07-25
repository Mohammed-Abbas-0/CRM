using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Domain.Entities
{
    public class CampaignCustomer
    {
        [Key]
        public int Id { get; set; }
        public int CampaignId { get; set; }
        public Campaign Campaign { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }

}
