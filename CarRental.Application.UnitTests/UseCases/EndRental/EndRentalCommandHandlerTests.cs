using CarRental.Application.UseCases.EndRental;
using CarRental.Application.UseCases.EndRental.Pricing;
using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;
using FluentResults;
using NSubstitute;

namespace CarRental.Application.UnitTests.UseCases.EndRental;

public class EndRentalCommandHandlerTests
{
    [Fact]
    public async Task
        GivenEndRentalCommand_WhenHandling_ThenInvokesRentalUpdateWithCorrectValuesAndReturnsPrice()
    {
        // Arrange
        const int rentalId = 1;
        const decimal expectedPrice = 1499.98m;
        var unitOfWork = Substitute.For<IUnitOfWork>();
        unitOfWork.RentalRepository.GetByIdAsync(rentalId).Returns(Result.Ok(new Rental()));
        unitOfWork.PricingDefinitionRepository.GetByIdAsync(Arg.Any<int>())
            .Returns(Result.Ok(new PricingDefinition()));
        var priceCalculator = Substitute.For<IPriceCalculator>();
        priceCalculator.GetRentalPrice(Arg.Any<Rental>(), Arg.Any<EndRentalCommand>())
            .Returns(Result.Ok(expectedPrice));
        unitOfWork.RentalRepository.UpdateAsync(Arg.Any<Rental>())
            .Returns(Result.Ok(new Rental { Price = expectedPrice }));
        var command = EndRentalCommand.Create(rentalId, 11000, DateTime.Now, 1).Value;
        var sut = new EndRentalCommandHandler(unitOfWork, priceCalculator);

        // Act
        var result = await sut.Handle(command, new CancellationToken());

        // Assert
        await unitOfWork.RentalRepository.Received(1).UpdateAsync(Arg.Is<Rental>(r =>
            r.End.Equals(command.End) && r.OdometerReadingAtReturn.Equals(command.OdometerReadingAtReturn) &&
            r.Price.Equals(expectedPrice)));
        Assert.True(result.IsSuccess);
        Assert.Equal(expectedPrice, result.Value.Price);
    }
}