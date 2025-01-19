using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;
using FluentResults;
using MediatR;

namespace CarRental.Application.UseCases.GetRental;

public class GetRentalQueryHandler : IRequestHandler<GetRentalQuery, Result<Rental>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetRentalQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Rental>> Handle(GetRentalQuery request, CancellationToken cancellationToken)
    {
        var rentalResult = await _unitOfWork.RentalRepository.GetByIdAsync(request.BookingNumber);
        if (rentalResult.IsFailed)
        {
            return Result.Fail<Rental>(rentalResult.Errors);
        }

        if (rentalResult.Value == null)
        {
            return Result.Fail<Rental>(new Error($"Rental with booking number {request.BookingNumber} not found"));
        }

        return Result.Ok(rentalResult.Value);
    }
}