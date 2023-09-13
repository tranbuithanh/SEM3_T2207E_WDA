using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.CQRS.Requests.Queries;

namespace OnionArchitecture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator mediator;
        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet(Name = "GetAllUserProfile")]
        public async Task<ActionResult<UserProfile>> GetAllUserProfile()
        {
            var response = await mediator.Send(new GetListUserProfileRequest() {});
            return Ok(response);
        }
    }
}
