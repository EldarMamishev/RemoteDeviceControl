using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SignalController : ControllerBase
    {
        private readonly IHubContext<SignalHub> _hubContext;
        public SignalController(IHubContext<SignalHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> SignalArrived(SignalInputModel inputModel)
        {
            await _hubContext.Clients.All.SendAsync("SignalMessageReceived", inputModel);
            return StatusCode(200, inputModel);

        }
    }
}
