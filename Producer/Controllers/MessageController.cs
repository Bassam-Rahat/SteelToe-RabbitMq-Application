using MediatR;
using Microsoft.AspNetCore.Mvc;
using Producer.Command;

namespace Producer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MessageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessageAsync()
        {
            var message = "Hello, Consumer! This is a hardcoded message.";
            var response = await _mediator.Send(new MessagePostCommand
            {
                Message = message
            });

            if(response == true)
                return Ok("Message sent successfully.");
            return BadRequest("Failed to send Message");
        }
    }
}
