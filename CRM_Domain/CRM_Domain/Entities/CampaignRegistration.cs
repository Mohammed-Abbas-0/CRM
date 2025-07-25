    using System.ComponentModel.DataAnnotations;

    namespace CRM_Domain.Entities
    {
        public class CampaignRegistration
        {
            [Key]
            public int Id { get; set; }  
            public required int CampaignId { get; set; }  
            public Campaign? Campaign { get; set; }  

            public required string UserId { get; set; }  
            public User? User { get; set; }

            public bool IsRegistered { get; set; } = true;
            public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
        }
    }
