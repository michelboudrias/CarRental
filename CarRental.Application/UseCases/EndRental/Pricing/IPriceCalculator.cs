using CarRental.Domain.Entities;
using FluentResults;

namespace CarRental.Application.UseCases.EndRental.Pricing;

public interface IPriceCalculator
{
    Task<Result<decimal>> GetRentalPrice(Rental rental, EndRentalCommand command);
}