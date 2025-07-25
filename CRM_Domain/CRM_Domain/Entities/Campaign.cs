namespace CRM_Domain.Entities
{
    public class Campaign
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal Budget { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public string? UserId { get; set; }
        public string? ImageUrl { get; set; } // 📸 استقبال الصورة هنا
        public byte[]? ImageData { get; set; }
        public decimal? RateDiscount { get; set; } // نسبة الخصم
        public decimal? BudgetAfterDiscount { get; set; } // الميزانية بعد الخصم


        // علاقة Many-to-Many مع العملاء
        public ICollection<CampaignCustomer> CampaignCustomers { get; set; }
    }
}
