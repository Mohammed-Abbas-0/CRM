namespace CRM_Interface.Dtos
{
    public class CampaignCommentDto
    {
        public required string Content { get; set; }
        public required string UserId { get; set; }
        public int CampaignId { get; set; }

    }

    public record CampaignCommentRecord(string Content,string UserId,string UserName);
}
