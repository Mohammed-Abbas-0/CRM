using CRM_Interface.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CRM_Application.Commands.Classes
{
    public class CreateCampaignCommand:IRequest<CampaignDto>
    {
        public required string Name { get; set; }
        public required string UserId { get; set; }
        public required string Description { get; set; }
        public decimal Budget { get; set; }
        public decimal? RateDiscount { get; set; } // نسبة الخصم
        public decimal? BudgetAfterDiscount { get; set; } // الميزانية بعد الخصم
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IFormFile? ImageUrl { get; set; } // 📸 استقبال الصورة هنا
    }
}
