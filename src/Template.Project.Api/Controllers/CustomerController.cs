using MediatR;
using Microsoft.AspNetCore.Mvc;
using Template.Project.Application.Customers.Add;
using Template.Project.Application.Customers.Get;
using Template.Project.Application.Customers.Update;

namespace Template.Project.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly IMediator _mediator;

        public CustomerController(ILogger<CustomerController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery] CustomerGetByIdQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] AddCustomerCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("{id}/update")]
        public async Task<IActionResult> UpdateName([FromRoute] string id, [FromBody] UpdateCustomerRequest request)
        {
            return Ok(await _mediator.Send(new UpdateCustomerCommand(id, request)));
        }
    }
}