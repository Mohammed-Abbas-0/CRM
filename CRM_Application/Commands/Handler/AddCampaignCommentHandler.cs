using CRM_Application.Commands.Classes;
using CRM_Interface.Dtos;
using CRM_Interface.IRepositories;
using MediatR;

namespace CRM_Application.Commands.Handler
{
    public class AddCampaignCommentHandler : IRequestHandler<AddCampaignCommentCommand, bool>
    {
        private readonly ICampaignRepository _campaignRepository;

        public AddCampaignCommentHandler(ICampaignRepository campaignRepository) => _campaignRepository = campaignRepository;

        public async Task<bool> Handle(AddCampaignCommentCommand request, CancellationToken cancellationToken)
        {
            var campaignCommentDto = new CampaignCommentDto
            {
                UserId = request.UserId,
                CampaignId = request.CampaignId,
                Content = request.Content,
                Rating = request.Rating
            };
            var addComment =  await _campaignRepository.AddCommentAsync(campaignCommentDto);
            return addComment;
        }
    }
}
