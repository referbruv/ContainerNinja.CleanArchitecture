using ContainerNinja.Contracts.Constants;
using ContainerNinja.Contracts.DTO;
using ContainerNinja.Core.Exceptions;
using ContainerNinja.Core.Handlers.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ContainerNinja.API.Controllers.V2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = UserRoles.Owner)]
        [MapToApiVersion("2.0")]
        [HttpPost, Route("create")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateOrUpdateUserDTO model)
        {
            try
            {
                var request = new CreateUserCommand(model);
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (InvalidRequestBodyException ex)
            {
                return BadRequest(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = ex.Errors
                });
            }
        }

        [AllowAnonymous]
        [MapToApiVersion("2.0")]
        [HttpPost, Route("token")]
        public async Task<IActionResult> ValidateAsync([FromBody] ValidateUserDTO model)
        {
            try
            {
                var request = new ValidateUserCommand(model);
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (InvalidRequestBodyException ex)
            {
                return BadRequest(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = ex.Errors
                });
            }
            catch (EntityNotFoundException ex)
            {
                return Unauthorized(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = new string[] { ex.Message }
                });
            }
        }
    }
}
