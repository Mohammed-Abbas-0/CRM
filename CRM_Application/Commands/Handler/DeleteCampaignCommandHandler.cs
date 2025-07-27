using CRM_Application.Commands.Classes;
using CRM_Interface.IRepositories;
using MediatR;

namespace CRM_Application.Commands.Handler
{
    internal class DeleteCampaignCommandHandler : IRequestHandler<DeleteCampaignCommand, bool>
    {
        private readonly ICampaignRepository _campaignRepository;

        public DeleteCampaignCommandHandler(ICampaignRepository campaignRepositor) => _campaignRepository = campaignRepositor;
        
        public async Task<bool> Handle(DeleteCampaignCommand request, CancellationToken cancellationToken) 
            => await _campaignRepository.DeleteAsync(request.Id);
        
    }
}
