namespace CRM_Interface.Dtos
{
    public class CampaignCommentDto
    {
        public required string Content { get; set; }
        public required string UserId { get; set; }
        public int CampaignId { get; set; }
        public int? Rating { get; set; } = 0;

    }

    public record CampaignCommentRecord(string Content,string UserId,string UserName,string CreatedAt,int? Rating=0);
}
