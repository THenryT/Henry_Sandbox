using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDWithRabbitMQServiceBus.EventBus.Abstract;
using DDDWithRabbitMQServiceBus.Events;
using Microsoft.AspNetCore.Mvc;

namespace DDDWithRabbitMQServiceBus.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly IEventBus _eventBus;
        public HomeController(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }
        [Route("publish")]
        public OkResult Index()
        {
            var testEvent = new TestEvent("Hello World !!");

            _eventBus.Publish(testEvent);

            return Ok();
        }
    }
}
