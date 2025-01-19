using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;
using CarRental.Infrastructure.Persistence;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.Repositories;

public class PricingDefinitionRepository : IPricingDefinitionRepository
{
    private readonly CarRentalDbContext _context;

    public PricingDefinitionRepository(CarRentalDbContext context)
    {
        _context = context;
    }

    public async Task<Result<PricingDefinition>> GetByIdAsync(int id)
    {
        try
        {
            var pricingDefinition = await _context.PricingDefinitions
                .Include(pd => pd.CarCategory)
                .SingleOrDefaultAsync(pd => pd.Id.Equals(id));
            return Result.Ok(pricingDefinition);
        }
        catch (Exception e)
        {
            return Result.Fail<PricingDefinition>(e.Message);
        }
    }

    public async Task<Result<List<PricingDefinition>>> GetAsync()
    {
        try
        {
            var pricingDefinitionsResult = await _context.PricingDefinitions
                .Include(pd => pd.CarCategory)
                .ToListAsync();
            return Result.Ok(pricingDefinitionsResult);
        }
        catch (Exception e)
        {
            return Result.Fail<List<PricingDefinition>>(e.Message);
        }
    }
}