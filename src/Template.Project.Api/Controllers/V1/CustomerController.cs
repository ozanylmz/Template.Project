using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Template.Project.Application.Customers.Add;
using Template.Project.Application.Customers.Cancel;
using Template.Project.Application.Customers.Get;
using Template.Project.Application.Customers.Patch;
using Template.Project.Application.Customers.Filter;
using Template.Project.Application.Customers.Update;

namespace Template.Project.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly IMediator _mediator;

        public CustomerController(ILogger<CustomerController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerGetByIdResponse))]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            return Ok(await _mediator.Send(new CustomerGetByIdQuery(id)));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FilterCustomerResponse>))]
        public async Task<IActionResult> Filter([FromQuery] CustomerFilterQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Insert([FromBody] AddCustomerCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateCustomerRequest request)
        {
            return Ok(await _mediator.Send(new UpdateCustomerCommand(id, request)));
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Patch([FromRoute] string id, [FromBody] JsonPatchDocument<PatchCustomerRequest> patchDoc)
        {
            return Ok(await _mediator.Send(new PatchCustomerCommand(id, patchDoc)));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Cancel([FromRoute] string id)
        {
            await _mediator.Send(new CancelCustomerCommand(id));
            return NoContent();
        }
    }
}