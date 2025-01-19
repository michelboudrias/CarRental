using CarRental.Application.UseCases.EndRental;
using CarRental.Application.UseCases.EndRental.Pricing;
using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;
using FluentResults;
using NSubstitute;

namespace CarRental.Application.UnitTests.UseCases.EndRental.Pricing;

public class PriceCalculatorTests
{
    [Theory]
    [MemberData(nameof(TestData))]
    public async Task GivenPricingDefinition_WhenGettingRentalPrice_ThenReturnsCorrectValue(PricingDefinition pricingDefinition, decimal expectedPrice)
    {
        // Arrange
        var startDate = DateTime.Parse("2025-01-01T08:00:00");
        var endDate = DateTime.Parse("2025-01-03T22:00:00");
        const int odometerReadingAtStart = 10000;
        const int odometerReadingAtReturn = 10500;

        var unitOfWork = Substitute.For<IUnitOfWork>();
        unitOfWork.PricingDefinitionRepository.GetByIdAsync(Arg.Any<int>())
            .Returns(Result.Ok(pricingDefinition));
        var sut = new PriceCalculator(unitOfWork);
        var rental = CreateRental(pricingDefinition.CarCategoryId, startDate, odometerReadingAtStart);
        var command = EndRentalCommand.Create(1, odometerReadingAtReturn, endDate, 1).Value;

        // Act
        var result = await sut.GetRentalPrice(rental, command);

        // Assert
        await unitOfWork.PricingDefinitionRepository.Received(1).GetByIdAsync(Arg.Any<int>());
        Assert.Equal(expectedPrice, result.Value);
    }

    public static IEnumerable<object[]> TestData()
    {
        yield return [new PricingDefinition
        {
            Id = 1,
            Name = "Small Car Formula",
            CarCategoryId = 1,
            PricePerDay = 500,
            PricePerDayMultiplier = 1,
            PricePerKilometer = 0,
            PricePerKilometerMultiplier = 0,
        }, 1000];
        yield return [new PricingDefinition
        {
            Id = 2,
            Name = "Combi Formula",
            CarCategoryId = 2,
            PricePerDay = 500,
            PricePerDayMultiplier = 1.3m,
            PricePerKilometer = 0.5m,
            PricePerKilometerMultiplier = 1,
        }, 1550];
        yield return [new PricingDefinition
        {
            Id = 3,
            Name = "Truck Formula",
            CarCategoryId = 3,
            PricePerDay = 500,
            PricePerDayMultiplier = 1.5m,
            PricePerKilometer = 0.5m,
            PricePerKilometerMultiplier = 1.5m
        }, 1875];
    }

    private static Rental CreateRental(int carCategoryId, DateTime startDate, int odometerReadingAtStart)
    {
        return new Rental
        {
            Start = startDate,
            OdometerReadingAtStart = odometerReadingAtStart,
            Car = new Car
            {
                Category = new CarCategory
                {
                    Id = carCategoryId
                }
            }
        };
    }
}