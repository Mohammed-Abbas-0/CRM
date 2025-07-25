using CRM_Application.Queries.Classes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ClientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        ///[Authorize]
        [HttpPost("getclients")]
        public async Task<IActionResult> GetClients([FromBody] GetClientsCommand getClientsCommand)
        {
            var response = await _mediator.Send(getClientsCommand);
            return Ok(response);
        }


        
    }
}
