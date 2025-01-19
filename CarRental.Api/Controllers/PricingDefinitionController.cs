using CarRental.Application.UseCases.GetPricingDefinitions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers;

public class PricingDefinitionController : BaseApiController
{
    public PricingDefinitionController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("List")]
    public async Task<IActionResult> GetPricingDefinitions()
    {
        var query = new GetPricingDefinitionsQuery();
        var result = await Mediator.Send(query);
        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Errors); //todo: return other status codes for different errors
    }
}