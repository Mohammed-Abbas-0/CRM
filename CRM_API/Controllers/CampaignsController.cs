using CRM_Application.Commands.Classes;
using CRM_Application.Queries.Classes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignsController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CampaignsController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        
        [HttpPost("createcampaign")]
        public async Task<IActionResult> CreateCampaign([FromForm] CreateCampaignCommand createCampaign)
        { 
            var result = await _mediator.Send(createCampaign);
            return Ok(result);
        }

        [HttpPatch("update")]
        public async Task<IActionResult> UpdateCampaign([FromBody] UpdateCampaignCommand updateCampaign)
        {
            var result = await _mediator.Send(updateCampaign);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("getallcampaigns")]
        public async Task<IActionResult> GetAllCampaigns([FromBody] GetAllCampaignCommand getAllCampaignCommand)
        {
            var result = await _mediator.Send(getAllCampaignCommand);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("getcampaignbyid/{id}")]
        public async Task<IActionResult> GetCampaignById(int id)
        {
            GetCampaignCommand getCampaignCommand = new() { Id = id };
            var result = await _mediator.Send(getCampaignCommand);
            return Ok(result);
        }


        [HttpPost("changecampaigninteraction")]
        public async Task<IActionResult> ChangeCampaignInteraction([FromBody] ChangeCampaignInteractionCommand interactionCommand)
        {
            var result = await _mediator.Send(interactionCommand);
            return Ok(result);
        }


        [HttpPost("registercampaign")]
        public async Task<IActionResult> RegisterCampaign([FromBody] RegisterCampaignCommand registerCampaignCommand)
        {
            var response = await _mediator.Send(registerCampaignCommand);
            return Ok(response);
        }

        [HttpPost("unregistercampaign")]
        public async Task<IActionResult> UnRegisterCampaign([FromBody] UnRegisterCampaignCommand unRegisterCampaignCommand)
        {
            var response = await _mediator.Send(unRegisterCampaignCommand);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("search")]
        public async Task<IActionResult> SearchCampaign([FromQuery] string searchKeyWord = "")
        {
            GetSearchCampaignCommand campaignCommand = new GetSearchCampaignCommand { SearchKeyWord = searchKeyWord };
            var response = await _mediator.Send(campaignCommand);
            return Ok(response);
        }

        #region Get All Comments

        [HttpGet("getallcomments")]
        public async Task<IActionResult> GetAllCampaignComments([FromQuery] int CampaignId)
        {
            GetAllCampaignCommentsCommand getAllCampaignCommentsCommand = new GetAllCampaignCommentsCommand { CampaignId = CampaignId };
            var response = await _mediator.Send(getAllCampaignCommentsCommand);
            return Ok(response);
        }

        #endregion

        #region Add Comment

        [HttpPost("addcomment")]
        public async Task<IActionResult> AddCampaignComment([FromBody] AddCampaignCommentCommand addCampaignCommentCommand)
        {
            if (string.IsNullOrWhiteSpace(addCampaignCommentCommand.Content))
                return BadRequest("Content is required");

            var response = await _mediator.Send(addCampaignCommentCommand);
            return Ok(response);
        }

        #endregion

        #region Delete

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var command = new DeleteCampaignCommand { Id = id };
            var success = await _mediator.Send(command);

            if (!success)
                return NotFound(); 

            return NoContent(); 
        }

        #endregion

        #region Get All Campaign which Created By  Company 

        [HttpGet("getallcampaignsbyuser")]
        public async Task<IActionResult> GetAllCampaignsByUser()
        {
            var userId = _httpContextAccessor?.HttpContext?.User.FindFirst("UID")?.Value;
            if (string.IsNullOrEmpty(userId))
                return BadRequest("User ID not found in token.");

            GetAllCampaignsByUserCommand campaignCommand = new GetAllCampaignsByUserCommand { UsertId = userId };
            var response = await _mediator.Send(campaignCommand);
            return Ok(response);

        }

        #endregion

    }
}
