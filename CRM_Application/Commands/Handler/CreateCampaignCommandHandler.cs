using CRM_Application.Commands.Classes;
using CRM_Interface.Dtos;
using CRM_Interface.IRepositories;
using MediatR;

namespace CRM_Application.Commands.Handler
{
    public class CreateCampaignCommandHandler:IRequestHandler<CreateCampaignCommand, CampaignDto>
    {
        private readonly ICampaignRepository _campaignRepository;
        public CreateCampaignCommandHandler(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
    }

        //public async Task<CampaignDto> Handle(CreateCampaignCommand request, CancellationToken cancellationToken)
        //{
        //    var compaign = new CampaignDto()
        //    { 
        //        Description = request.Description,
        //        Name = request.Name,
        //        StartDate = DateTime.Now,
        //        EndDate = request.EndDate,
        //        Budget = request.Budget,
        //        UserId = request.UserId,
        //        ImageUrl = request.ImageUrl,

        //    };

        //    var result = await _campaignRepository.AddAsync(compaign);

        //    return result? compaign :new CampaignDto() { UserId = request.UserId,Description="",Name="",StartDate=DateTime.Now,EndDate=DateTime.Now,Budget=0,Id=0 };
        //}
        public async Task<CampaignDto> Handle(CreateCampaignCommand request, CancellationToken cancellationToken)
        {
            string? imageUrl = null;

            if (request.ImageUrl != null && request.ImageUrl.Length > 0)
            {
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var fileName = Guid.NewGuid() + Path.GetExtension(request.ImageUrl.FileName);
                var fullPath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await request.ImageUrl.CopyToAsync(stream, cancellationToken);
                }

                imageUrl = $"/images/{fileName}";
            }
            byte[]? imageData = null;

            if (request.ImageUrl != null && request.ImageUrl.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await request.ImageUrl.CopyToAsync(ms, cancellationToken);
                    imageData = ms.ToArray();
                }
            }

            var campaign = new CampaignDto
            {
                Name = request.Name,
                Description = request.Description,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Budget = request.Budget,
                UserId = request.UserId,
                ImageUrl = imageUrl,
                ImageData = imageData,
                BudgetAfterDiscount = request.BudgetAfterDiscount,
                RateDiscount = request.RateDiscount
            };

            await _campaignRepository.AddAsync(campaign);
            return campaign;
        }

    }
}
