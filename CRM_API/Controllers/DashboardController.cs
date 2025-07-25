using CRM_Application.Queries.Classes;
using CRM_Interface.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DashboardController(IMediator mediator) => _mediator = mediator;


        [HttpPost("campaingslikedata")]
        public async Task<IActionResult> GetCampaignsLikesAsync([FromBody] GetCampaignsLikesCommand getCampaignsLikes)
        {
            var response = await _mediator.Send(getCampaignsLikes);
            return Ok(response);
        }

        [HttpPost("campaingsregisterdata")]
        public async Task<IActionResult> GetCampaignsRegisterationAsync([FromBody]  GetCampaignsRegisterationCommand getCampaignsRegisteration)
        {
            var response = await _mediator.Send(getCampaignsRegisteration);
            return Ok(response);
        }


        [HttpPost("getcampaignsinteraction")]
        public async Task<IActionResult> GetCampaignsInteractionAsync([FromBody] GetCampaignsInteractionCommand getCampaignsInteraction)
        {
            var response = await _mediator.Send(getCampaignsInteraction);
            return Ok(response);
        }

        [HttpPost("getclientchart")]
        public async Task<IActionResult> GetClientChartAsync([FromBody] GetClientChartCommand getClientChartCommand)
        {
            var response = await _mediator.Send(getClientChartCommand);
            return Ok(response);
        }
    }
}
