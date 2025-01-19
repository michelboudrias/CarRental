using CarRental.Api.DataTransferObjects;
using CarRental.Application.UseCases.EndRental;
using CarRental.Application.UseCases.GetRental;
using CarRental.Application.UseCases.StartRental;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RentalController : BaseApiController
{
    public RentalController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost("Start")]
    public async Task<IActionResult> StartRental(StartRentalDto dto)
    {
        var command = StartRentalCommand.Create(dto.CarRegistrationNumber, dto.DriverSSN, dto.Start,
            dto.OdometerReadingAtStart);
        if (command.IsFailed)
        {
            return BadRequest(command.Errors);
        }

        var result = await Mediator.Send(command.Value);
        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Errors); //todo: return other status codes for different errors
    }

    [HttpPost("End/{bookingNumber}")]
    public async Task<IActionResult> EndRental([FromRoute] int bookingNumber, EndRentalDto dto)
    {
        var command =
            EndRentalCommand.Create(bookingNumber, dto.OdometerReadingAtReturn, dto.End, dto.PriceDefinitionId);
        if (command.IsFailed)
        {
            return BadRequest(command.Errors);
        }

        var result = await Mediator.Send(command.Value);
        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Errors); //todo: return other status codes for different errors
    }

    [HttpGet("{bookingNumber}")]
    public async Task<IActionResult> GetRental([FromRoute] int bookingNumber)
    {
        var query = GetRentalQuery.Create(bookingNumber);
        if (query.IsFailed)
        {
            return BadRequest(query.Errors);
        }

        var result = await Mediator.Send(query.Value);
        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Errors); //todo: return other status codes for different errors
    }
}