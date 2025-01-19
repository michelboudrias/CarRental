using CarRental.Domain.Entities;
using FluentResults;
using MediatR;

namespace CarRental.Application.UseCases.GetRental;

public class GetRentalQuery : IRequest<Result<Rental>>
{
    public int BookingNumber { get; }

    private GetRentalQuery(int bookingNumber)
    {
        BookingNumber = bookingNumber;
    }

    public static Result<GetRentalQuery> Create(int bookingNumber)
    {
        if (bookingNumber <= 0)
        {
            return Result.Fail<GetRentalQuery>("Id must be greater than 0");
        }

        return Result.Ok(new GetRentalQuery(bookingNumber));
    }
}