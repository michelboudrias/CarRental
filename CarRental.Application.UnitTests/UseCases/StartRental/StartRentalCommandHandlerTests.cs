using CarRental.Application.UseCases.StartRental;
using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;
using FluentResults;
using NSubstitute;

namespace CarRental.Application.UnitTests.UseCases.StartRental;

public class StartRentalCommandHandlerTests
{
    [Fact]
    public async Task
        GivenStartRentalCommand_WhenHandling_ThenAddRentalToRepositoryWithCorrectValuesAndReturnsBookingNumber()
    {
        // Arrange
        const int carId = 1;
        const string registrationNumber = "ABC123";
        var unitOfWork = Substitute.For<IUnitOfWork>();
        unitOfWork.CarRepository.GetByRegistrationNumberAsync(registrationNumber)
            .Returns(new Car { Id = carId });
        unitOfWork.RentalRepository.AddAsync(Arg.Any<Rental>())
            .Returns(Result.Ok(1));
        var sut = new StartRentalCommandHandler(unitOfWork);
        var command = StartRentalCommand
            .Create(registrationNumber, "197709168912", DateTime.Now, 1000)
            .Value;

        // Act
        var result = await sut.Handle(command, new CancellationToken());

        // Assert
        await unitOfWork.RentalRepository.Received(1)
            .AddAsync(Arg.Is<Rental>(r =>
                r.CarId.Equals(carId) &&
                r.DriverSSN.Equals(command.DriverSSN) &&
                r.Start.Equals(command.Start) &&
                r.OdometerReadingAtStart.Equals(command.OdometerReadingAtStart)));
        Assert.True(result.IsSuccess);
        Assert.IsType<int>(result.Value.BookingNumber);
    }
}