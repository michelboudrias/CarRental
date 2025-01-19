using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;
using FluentResults;

namespace CarRental.Application.UseCases.EndRental.Pricing;

public class PriceCalculator : IPriceCalculator
{
    private readonly IUnitOfWork _unitOfWork;

    public PriceCalculator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<decimal>> GetRentalPrice(Rental rental, EndRentalCommand command)
    {
        var pricingDefinitionResult =
            await _unitOfWork.PricingDefinitionRepository.GetByIdAsync(command.PriceDefinitionId);

        if (pricingDefinitionResult.IsFailed)
        {
            return Result.Fail<decimal>(pricingDefinitionResult.Errors);
        }

        var pricingDefinition = pricingDefinitionResult.Value;
        if (pricingDefinition == null)
        {
            return Result.Fail<decimal>(new Error($"Price definition with id {command.PriceDefinitionId} not found"));
        }

        if (!pricingDefinition.CarCategoryId.Equals(rental.Car.Category.Id))
        {
            return Result.Fail<decimal>(new Error("Price definition does not match car category"));
        }

        var rentalDays = (command.End - rental.Start).Days < 1
            ? 1
            : (command.End - rental.Start).Days;

        return (pricingDefinition.PricePerDay * rentalDays * pricingDefinition.PricePerDayMultiplier) +
               (pricingDefinition.PricePerKilometer *
                (command.OdometerReadingAtReturn - rental.OdometerReadingAtStart) *
                pricingDefinition.PricePerKilometerMultiplier);
    }
}