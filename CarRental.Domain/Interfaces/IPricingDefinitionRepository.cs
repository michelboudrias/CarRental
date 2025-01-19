using CarRental.Domain.Entities;
using FluentResults;

namespace CarRental.Domain.Interfaces;

public interface IPricingDefinitionRepository
{
    Task<Result<PricingDefinition>> GetByIdAsync(int id);
    Task<Result<List<PricingDefinition>>> GetAsync();
}