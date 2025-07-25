namespace CRM_Interface.Dtos
{
    // Create / Update
    public class CampaignDto
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal Budget { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public string? ImageUrl { get; set; } // 📸 استقبال الصورة هنا
        public byte[]? ImageData { get; set; }
        public decimal? RateDiscount { get; set; } // نسبة الخصم
        public decimal? BudgetAfterDiscount { get; set; } // الميزانية بعد الخصم

    }
    // Get

    public record CampaignRecord(int? Id=0,string Name="",string Description="",decimal Budget=0,string UserId="",string UserName="",
                                                    DateTime? StartDate= null, DateTime? EndDate= null,bool? IsRegistered=false,int? LikesCount=0,
                                                    bool? IsLiked = false,string? ImageUrl="", byte[]? ImageData = null, decimal? RateDiscount=0,decimal? BudgetAfterDiscount=0);

}
