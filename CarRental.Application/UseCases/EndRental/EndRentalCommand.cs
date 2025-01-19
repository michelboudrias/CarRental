using FluentResults;
using MediatR;

namespace CarRental.Application.UseCases.EndRental;

public class EndRentalCommand : IRequest<Result<EndRentalCommandResponse>>
{
    public int BookingNumber { get; }
    public int OdometerReadingAtReturn { get; }
    public DateTime End { get; }
    public int PriceDefinitionId { get; }

    private EndRentalCommand(int bookingNumber, int odometerReadingAtReturn, DateTime end, int priceDefinitionId)
    {
        BookingNumber = bookingNumber;
        OdometerReadingAtReturn = odometerReadingAtReturn;
        End = end;
        PriceDefinitionId = priceDefinitionId;
    }

    public static Result<EndRentalCommand> Create(int bookingNumber, int odometerReadingAtReturn, DateTime end,
        int priceDefinitionId)
    {
        //todo: validate end date is not min value
        return Result.Ok(new EndRentalCommand(bookingNumber, odometerReadingAtReturn, end, priceDefinitionId));
    }
}