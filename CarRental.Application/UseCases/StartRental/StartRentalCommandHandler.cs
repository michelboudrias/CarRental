using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;
using FluentResults;
using MediatR;

namespace CarRental.Application.UseCases.StartRental;

public class StartRentalCommandHandler : IRequestHandler<StartRentalCommand, Result<StartRentalCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public StartRentalCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<StartRentalCommandResponse>> Handle(StartRentalCommand command,
        CancellationToken cancellationToken)
    {
        var carResult = await _unitOfWork.CarRepository.GetByRegistrationNumberAsync(command.CarRegistrationNumber);

        if (carResult.IsFailed)
        {
            return Result.Fail<StartRentalCommandResponse>(carResult.Errors);
        }

        var car = carResult.Value;
        if (car == null)
        {
            return Result.Fail<StartRentalCommandResponse>(
                new Error($"Car with registration number {command.CarRegistrationNumber} not found"));
        }
        //todo: validate that the car is not already rented

        var rental = new Rental
        {
            CarId = car.Id,
            DriverSSN = command.DriverSSN,
            Start = command.Start,
            OdometerReadingAtStart = command.OdometerReadingAtStart
        };
        var rentalResult = await _unitOfWork.RentalRepository.AddAsync(rental);

        return rentalResult.IsSuccess
            ? Result.Ok(new StartRentalCommandResponse(rentalResult.Value))
            : Result.Fail<StartRentalCommandResponse>(rentalResult.Errors);
    }
}

public record StartRentalCommandResponse(int BookingNumber);