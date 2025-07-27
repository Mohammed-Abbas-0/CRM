using MediatR;

namespace CRM_Application.Commands.Classes
{
    public class DeleteCampaignCommand:IRequest<bool>
    {
        public int Id { get; set; }
    }
}
