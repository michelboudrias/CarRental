using FluentResults;
using MediatR;

namespace CarRental.Application.UseCases.StartRental;

public class StartRentalCommand : IRequest<Result<StartRentalCommandResponse>>
{
    public string CarRegistrationNumber { get; }
    public string DriverSSN { get; }
    public DateTime Start { get; }
    public int OdometerReadingAtStart { get; }

    private StartRentalCommand(string carRegistrationNumber, string driverSSN,
        DateTime start, int odometerReadingAtStart)
    {
        CarRegistrationNumber = carRegistrationNumber;
        DriverSSN = driverSSN;
        Start = start;
        OdometerReadingAtStart = odometerReadingAtStart;
    }

    public static Result<StartRentalCommand> Create(string carRegistrationNumber, string driverSSN,
        DateTime start, int odometerReadingAtStart)
    {
        //todo: validate format of driver ssn

        return Result.Ok(new StartRentalCommand(carRegistrationNumber, driverSSN, start,
            odometerReadingAtStart));
    }
}