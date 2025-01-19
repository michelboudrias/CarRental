using CarRental.Application.UseCases.EndRental.Pricing;
using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;
using FluentResults;
using MediatR;

namespace CarRental.Application.UseCases.EndRental;

public class EndRentalCommandHandler : IRequestHandler<EndRentalCommand, Result<EndRentalCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPriceCalculator _priceCalculator;

    public EndRentalCommandHandler(IUnitOfWork unitOfWork, IPriceCalculator priceCalculator)
    {
        _unitOfWork = unitOfWork;
        _priceCalculator = priceCalculator;
    }

    public async Task<Result<EndRentalCommandResponse>> Handle(EndRentalCommand command,
        CancellationToken cancellationToken)
    {
        var rentalResult = await _unitOfWork.RentalRepository.GetByIdAsync(command.BookingNumber);
        if (rentalResult.IsFailed)
        {
            return Result.Fail<EndRentalCommandResponse>(rentalResult.Errors);
        }

        var rental = rentalResult.Value;
        var validationResult = Validate(command, rental);
        if (validationResult.IsFailed)
        {
            return Result.Fail<EndRentalCommandResponse>(validationResult.Errors);
        }

        var price = await _priceCalculator.GetRentalPrice(rental, command);
        if (price.IsFailed)
        {
            return Result.Fail<EndRentalCommandResponse>(price.Errors);
        }

        rental.End = command.End;
        rental.OdometerReadingAtReturn = command.OdometerReadingAtReturn;
        rental.Price = price.Value;

        var updateResult = await _unitOfWork.RentalRepository.UpdateAsync(rental);

        var registeredPrice = updateResult.Value.Price;
        if (!registeredPrice.HasValue)
        {
            return Result.Fail<EndRentalCommandResponse>(new Error("Price not registered"));
        }

        return updateResult.IsSuccess
            ? Result.Ok(new EndRentalCommandResponse(registeredPrice.Value))
            : Result.Fail<EndRentalCommandResponse>(updateResult.Errors);
    }

    private static Result<Rental> Validate(EndRentalCommand command, Rental? rental)
    {
        if (rental == null)
        {
            return Result.Fail<Rental>(new Error($"Rental with id {command.BookingNumber} not found"));
        }

        if (rental.End.HasValue)
        {
            return Result.Fail<Rental>(new Error("Rental is already closed"));
        }

        if (command.End < rental.Start)
        {
            return Result.Fail<Rental>(new Error("End date must be after start date"));
        }

        if (command.OdometerReadingAtReturn < rental.OdometerReadingAtStart)
        {
            return Result.Fail<Rental>(
                new Error("Odometer reading at return must be greater than odometer reading at start"));
        }

        return Result.Ok(rental);
    }
}

public record EndRentalCommandResponse(decimal? Price);