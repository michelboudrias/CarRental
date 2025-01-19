using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers;

[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
    public IMediator Mediator;

    public BaseApiController(IMediator mediator)
    {
        Mediator = mediator;
    }
}