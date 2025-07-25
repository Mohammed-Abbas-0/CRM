using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }

        // علاقة Many-to-Many مع الحملة
        public ICollection<CampaignCustomer> CampaignCustomers { get; set; }
    }

}
