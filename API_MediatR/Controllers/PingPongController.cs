using System;
using System.Threading.Tasks;
using API_MediatR.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API_MediatR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PingPongController : Controller
    {
        private readonly IMediator _mediator;
        
        public PingPongController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET
        [HttpGet]
        public async Task<IActionResult> Ping()
        {
            var response = await _mediator.Send(new Ping());
            return Ok(response);
        }
    }
}