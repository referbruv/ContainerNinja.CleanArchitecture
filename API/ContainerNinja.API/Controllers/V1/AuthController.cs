using ContainerNinja.Contracts.Constants;
using ContainerNinja.Contracts.DTO;
using ContainerNinja.Core.Exceptions;
using ContainerNinja.Core.Handlers.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace ContainerNinja.API.Controllers.V1
{
    [ApiVersion("1.0")]
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
        [MapToApiVersion("1.0")]
        [HttpPost, Route("create")]
        [ProducesResponseType(typeof(AuthTokenDTO), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> CreateAsync([FromBody] CreateOrUpdateUserDTO model)
        {
            try
            {
                var request = new CreateUserCommand(model);
                var result = await _mediator.Send(request);
                return StatusCode((int)HttpStatusCode.Created, result);
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
        [MapToApiVersion("1.0")]
        [HttpPost, Route("token")]
        [ProducesResponseType(typeof(AuthTokenDTO), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
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
