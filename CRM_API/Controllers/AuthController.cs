using CRM_Application.Commands.Classes;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand registerUserCommand)
        {
            var response = await _mediator.Send(registerUserCommand);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand loginUserCommand)
        {
            var response = await _mediator.Send(loginUserCommand);
            return Ok(response);
        }

        [HttpPost("addrole")]
        public async Task<IActionResult> AddRole([FromBody] AddRoleCommand addRoleCommand)
        {
            var response = await _mediator.Send(addRoleCommand);
            return Ok(response);
        }
    }
}
