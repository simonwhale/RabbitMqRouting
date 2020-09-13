using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WriteToQueue.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmitMessagesController : ControllerBase
    {
        [HttpPost]
        public IActionResult SendError()
        {
            var messageProcessor = new Logic.MessageProcessor();
            var response = messageProcessor.SendMessage("error", "This is a error message");
            if (response == StatusCodes.Status200OK)
                return Ok("Message sent");

            return BadRequest("Message Not Sent");
        }

        [HttpPost]
        public IActionResult SendWarning()
        {
            var messageProcessor = new Logic.MessageProcessor();
            var response = messageProcessor.SendMessage("warning", "This is a warning");
            if (response == StatusCodes.Status200OK)
                return Ok("Message Sent");

            return BadRequest("Message Not Sent");
        }
    }
}