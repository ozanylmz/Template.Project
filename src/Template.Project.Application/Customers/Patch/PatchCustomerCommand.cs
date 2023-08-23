using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace Template.Project.Application.Customers.Patch
{
    public sealed record PatchCustomerCommand(string Id, JsonPatchDocument<PatchCustomerRequest> Request) : IRequest
    {
    }
}
